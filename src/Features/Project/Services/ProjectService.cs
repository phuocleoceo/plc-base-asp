using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectMember.Services;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Project.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Shared.Enums;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Project.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IPermissionHelper _permissionHelper;
    private readonly IProjectMemberService _projectMemberService;
    private readonly IProjectPermissionService _projectPermissionService;

    public ProjectService(
        IUnitOfWork uow,
        IMapper mapper,
        IPermissionHelper permissionHelper,
        IProjectMemberService projectMemberService,
        IProjectPermissionService projectPermissionService
    )
    {
        _uow = uow;
        _mapper = mapper;
        _permissionHelper = permissionHelper;
        _projectMemberService = projectMemberService;
        _projectPermissionService = projectPermissionService;
    }

    public async Task<PagedList<ProjectDTO>> GetProjectsForUser(
        ReqUser reqUser,
        ProjectParams projectParams
    )
    {
        List<int> projectIds = await _uow.ProjectMember.GetProjectIdsForUser(reqUser.Id);

        QueryModel<ProjectEntity> projectQuery = new QueryModel<ProjectEntity>()
        {
            OrderBy = c => c.OrderByDescending(p => p.CreatedAt),
            Filters = { p => projectIds.Contains(p.Id) && p.DeletedAt == null },
            Includes = { p => p.Leader.UserProfile },
            PageSize = projectParams.PageSize,
            PageNumber = projectParams.PageNumber
        };

        if (!string.IsNullOrWhiteSpace(projectParams.SearchValue))
        {
            string searchValue = projectParams.SearchValue.ToLower();
            projectQuery.Filters.Add(
                p => p.Name.ToLower().Contains(searchValue) || p.Key.ToLower().Contains(searchValue)
            );
        }

        return await _uow.Project.GetPagedAsync<ProjectDTO>(projectQuery);
    }

    public async Task<ProjectDTO> GetProjectById(ReqUser reqUser, int projectId)
    {
        ProjectMemberEntity projectMember =
            await _uow.ProjectMember.GetOneAsync<ProjectMemberEntity>(
                new QueryModel<ProjectMemberEntity>()
                {
                    Filters =
                    {
                        m =>
                            m.UserId == reqUser.Id
                            && m.ProjectId == projectId
                            && m.DeletedAt == null
                    },
                }
            );

        if (projectMember == null)
            throw new BaseException(HttpCode.BAD_REQUEST, "unreachable_project");

        return await _uow.Project.GetOneAsync<ProjectDTO>(
            new QueryModel<ProjectEntity>()
            {
                Filters = { m => m.Id == projectId && m.DeletedAt == null },
                Includes = { p => p.Leader.UserProfile },
            }
        );
    }

    public async Task<bool> CreateProject(ReqUser reqUser, CreateProjectDTO createProjectDTO)
    {
        try
        {
            await _uow.CreateTransaction();

            double freeProject = await _uow.ConfigSetting.GetValueByKey(
                ConfigSettingKey.FREE_PROJECT
            );

            int projectCount = await _uow.Project.CountByCreatorId(reqUser.Id);

            if (projectCount >= freeProject)
            {
                // Payment
                double projectPrice = await _uow.ConfigSetting.GetValueByKey(
                    ConfigSettingKey.PROJECT_PRICE
                );

                if (!await _uow.UserProfile.MakePayment(reqUser.Id, projectPrice))
                    throw new BaseException(HttpCode.BAD_REQUEST, "not_enough_credit");
                await _uow.Save();
            }

            ProjectEntity projectEntity = _mapper.Map<ProjectEntity>(createProjectDTO);
            projectEntity.CreatorId = reqUser.Id;
            projectEntity.LeaderId = reqUser.Id;

            _uow.Project.Add(projectEntity);
            await _uow.Save();

            // Project member
            _uow.ProjectMember.Add(
                new ProjectMemberEntity() { UserId = reqUser.Id, ProjectId = projectEntity.Id, }
            );

            // Project Status
            _uow.ProjectStatus.Add(
                new ProjectStatusEntity()
                {
                    Name = "To Do",
                    Index = 0,
                    ProjectId = projectEntity.Id
                }
            );

            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException)
        {
            await _uow.AbortTransaction();
            throw;
        }
    }

    public async Task<bool> UpdateProject(
        ReqUser reqUser,
        int projectId,
        UpdateProjectDTO updateProjectDTO
    )
    {
        ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser.Id, projectId);

        if (projectDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");

        _mapper.Map(updateProjectDTO, projectDb);
        _uow.Project.Update(projectDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteProject(ReqUser reqUser, int projectId)
    {
        try
        {
            await _uow.CreateTransaction();

            ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser.Id, projectId);

            if (projectDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");

            _uow.Project.SoftDelete(projectDb);
            await _uow.Save();

            await _uow.ProjectMember.SoftDeleteMemberForProject(projectId);
            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException)
        {
            await _uow.AbortTransaction();
            throw;
        }
    }

    public async Task<IEnumerable<string>> GetPermissionsInProjectForUser(
        ReqUser reqUser,
        int projectId
    )
    {
        ProjectEntity projectEntity = await _uow.Project.FindByIdAsync(projectId);

        if (projectEntity == null)
        {
            throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");
        }

        if (projectEntity.LeaderId == reqUser.Id)
        {
            return _permissionHelper.GetAllPermissions().Select(p => p.Key);
        }

        return await _projectPermissionService.GetPermissionKeysOfRole(
            await _uow.ProjectMember.GetRoleInProjectForUser(reqUser.Id, projectId)
        );
    }
}

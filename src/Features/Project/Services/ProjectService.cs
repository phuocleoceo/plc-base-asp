using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Project.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Project.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
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
            projectQuery.Filters.Add(
                p =>
                    p.Name.ToLower().Contains(projectParams.SearchValue)
                    || p.Key.ToLower().Contains(projectParams.SearchValue)
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
            ProjectEntity projectEntity = _mapper.Map<ProjectEntity>(createProjectDTO);
            projectEntity.CreatorId = reqUser.Id;
            projectEntity.LeaderId = reqUser.Id;

            await _uow.CreateTransaction();

            _uow.Project.Add(projectEntity);
            await _uow.Save();

            _uow.ProjectMember.Add(
                new ProjectMemberEntity() { UserId = reqUser.Id, ProjectId = projectEntity.Id, }
            );
            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public async Task<bool> UpdateProject(
        ReqUser reqUser,
        int projectId,
        UpdateProjectDTO updateProjectDTO
    )
    {
        ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser, projectId);

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
            ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser, projectId);

            if (projectDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");

            await _uow.CreateTransaction();

            _uow.Project.SoftDelete(projectDb);
            await _uow.Save();

            await _uow.ProjectMember.SoftDeleteMemberForProject(projectId);
            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }
}

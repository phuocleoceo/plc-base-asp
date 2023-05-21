using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Project.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

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

    public async Task<List<ProjectDTO>> GetProjectsForUser(ReqUser reqUser)
    {
        List<int> projectIds = await _uow.ProjectMember.GetProjectIdsForUser(reqUser.Id);

        return await _uow.Project.GetManyAsync<ProjectDTO>(
            new QueryModel<ProjectEntity>()
            {
                OrderBy = c => c.OrderByDescending(p => p.CreatedAt),
                Filters = { p => projectIds.Contains(p.Id) && p.DeletedAt == null },
            }
        );
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
                    }
                }
            );

        if (projectMember == null)
            throw new BaseException(HttpCode.BAD_REQUEST, "unreachable_project");

        return await _uow.Project.GetOneAsync<ProjectDTO>(
            new QueryModel<ProjectEntity>()
            {
                Filters = { m => m.Id == projectId && m.DeletedAt == null }
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

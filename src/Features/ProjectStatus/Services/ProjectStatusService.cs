using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

namespace PlcBase.Features.ProjectStatus.Services;

public class ProjectStatusService : IProjectStatusService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectStatusService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<ProjectStatusDTO>> GetProjectStatusForProject(int projectId)
    {
        return await _uow.ProjectStatus.GetManyAsync<ProjectStatusDTO>(
            new QueryModel<ProjectStatusEntity>()
            {
                OrderBy = c => c.OrderBy(s => s.Index),
                Filters = { s => s.ProjectId == projectId && s.DeletedAt == null },
            }
        );
    }

    public async Task<bool> CreateProjectStatus(
        int projectId,
        CreateProjectStatusDTO createProjectStatusDTO
    )
    {
        ProjectStatusEntity projectStatusEntity = _mapper.Map<ProjectStatusEntity>(
            createProjectStatusDTO
        );
        projectStatusEntity.Index = await _uow.ProjectStatus.GetIndexForNewStatus(projectId);
        projectStatusEntity.ProjectId = projectId;

        _uow.ProjectStatus.Add(projectStatusEntity);
        return await _uow.Save();
    }

    public async Task<bool> UpdateProjectStatus(
        int projectId,
        int projectStatusId,
        UpdateProjectStatusDTO updateProjectStatusDTO
    )
    {
        ProjectStatusEntity projectStatusDb =
            await _uow.ProjectStatus.GetOneAsync<ProjectStatusEntity>(
                new QueryModel<ProjectStatusEntity>()
                {
                    Filters = { s => s.Id == projectStatusId && s.ProjectId == projectId },
                }
            );

        if (projectStatusDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_status_not_found");

        _mapper.Map(updateProjectStatusDTO, projectStatusDb);
        _uow.ProjectStatus.Update(projectStatusDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteProjectStatus(int projectId, int projectStatusId)
    {
        try
        {
            await _uow.CreateTransaction();

            int countStatus = await _uow.ProjectStatus.CountAsync(ps => ps.ProjectId == projectId);
            if (countStatus <= 1)
                throw new BaseException(HttpCode.BAD_REQUEST, "must_have_at_least_one_status");

            ProjectStatusEntity currentStatus = await _uow.ProjectStatus.FindByIdAsync(
                projectStatusId
            );

            if (currentStatus == null)
                throw new BaseException(HttpCode.NOT_FOUND, "project_status_not_found");

            // Update new status for issues
            int? newStatusId = await _uow.ProjectStatus.GetNewStatusIdForIssueWhenDeletingStatus(
                projectId,
                currentStatus.Id
            );

            List<IssueEntity> issues = await _uow.Issue.GetManyAsync<IssueEntity>(
                new QueryModel<IssueEntity>()
                {
                    Filters =
                    {
                        i =>
                            i.ProjectId == projectId
                            && i.ProjectStatusId == projectStatusId
                            && i.DeletedAt == null
                    }
                }
            );

            double statusIndex = _uow.Issue.GetStatusIndexForNewIssue(projectId, newStatusId.Value);
            foreach (IssueEntity issue in issues)
            {
                issue.ProjectStatusId = newStatusId;
                issue.ProjectStatusIndex = statusIndex++;
                _uow.Issue.Update(issue);
            }
            await _uow.Save();

            // Remove status
            _uow.ProjectStatus.Remove(currentStatus);
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

using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.ProjectStatus.DTOs;
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
            throw new BaseException(HttpCode.BAD_REQUEST, "project_status_not_found");

        _mapper.Map(updateProjectStatusDTO, projectStatusDb);
        _uow.ProjectStatus.Update(projectStatusDb);
        return await _uow.Save();
    }

    public async Task<bool> UpdateStatusIndex(
        int projectId,
        UpdateStatusIndexDTO updateStatusIndexDTO
    )
    {
        List<ProjectStatusEntity> projectStatusesDb =
            await _uow.ProjectStatus.GetManyAsync<ProjectStatusEntity>(
                new QueryModel<ProjectStatusEntity>()
                {
                    Filters = { s => s.ProjectId == projectId }
                }
            );

        if (projectStatusesDb.Count == 0)
            throw new BaseException(HttpCode.BAD_REQUEST, "project_statuses_not_found");

        if (projectStatusesDb.Count != updateStatusIndexDTO.NewIndexes.Count)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_status_indexes");

        for (int i = 0; i < updateStatusIndexDTO.NewIndexes.Count; i++)
        {
            ProjectStatusEntity currentStatus = projectStatusesDb.FirstOrDefault(
                s => s.Id == updateStatusIndexDTO.NewIndexes[i]
            );
            currentStatus.Index = i;
            _uow.ProjectStatus.Update(currentStatus);
        }

        return await _uow.Save();
    }
}

using AutoMapper;

using PlcBase.Common.Repositories;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Features.ProjectStatus.Entities;

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
}

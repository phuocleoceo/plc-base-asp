using PlcBase.Features.ProjectStatus.DTOs;

namespace PlcBase.Features.ProjectStatus.Services;

public interface IProjectStatusService
{
    Task<bool> CreateProjectStatus(int projectId, CreateProjectStatusDTO createProjectStatusDTO);
}

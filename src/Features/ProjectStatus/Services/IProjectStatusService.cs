using PlcBase.Features.ProjectStatus.DTOs;

namespace PlcBase.Features.ProjectStatus.Services;

public interface IProjectStatusService
{
    Task<List<ProjectStatusDTO>> GetProjectStatusForProject(int projectId);

    Task<bool> CreateProjectStatus(int projectId, CreateProjectStatusDTO createProjectStatusDTO);

    Task<bool> UpdateProjectStatus(
        int projectId,
        int projectStatusId,
        UpdateProjectStatusDTO updateProjectStatusDTO
    );

    Task<bool> DeleteProjectStatus(int projectId, int projectStatusId);
}

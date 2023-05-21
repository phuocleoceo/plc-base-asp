using PlcBase.Features.Project.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Project.Services;

public interface IProjectService
{
    Task<List<ProjectDTO>> GetProjectsForUser(ReqUser reqUser);

    Task<ProjectDTO> GetProjectById(ReqUser reqUser, int projectId);

    Task<bool> CreateProject(ReqUser reqUser, CreateProjectDTO createProjectDTO);

    Task<bool> UpdateProject(ReqUser reqUser, int projectId, UpdateProjectDTO updateProjectDTO);

    Task<bool> DeleteProject(ReqUser reqUser, int projectId);
}

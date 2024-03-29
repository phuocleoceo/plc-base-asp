using PlcBase.Features.Project.DTOs;
using PlcBase.Base.DomainModel;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Project.Services;

public interface IProjectService
{
    Task<PagedList<ProjectDTO>> GetProjectsForUser(ReqUser reqUser, ProjectParams projectParams);

    Task<ProjectDTO> GetProjectById(ReqUser reqUser, int projectId);

    Task<bool> CreateProject(ReqUser reqUser, CreateProjectDTO createProjectDTO);

    Task<bool> UpdateProject(ReqUser reqUser, int projectId, UpdateProjectDTO updateProjectDTO);

    Task<bool> DeleteProject(ReqUser reqUser, int projectId);

    Task<IEnumerable<string>> GetPermissionsInProjectForUser(ReqUser reqUser, int projectId);
}

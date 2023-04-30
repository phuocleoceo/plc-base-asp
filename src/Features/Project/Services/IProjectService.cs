using PlcBase.Features.Project.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Project.Services;

public interface IProjectService
{
    Task<bool> CreateProject(ReqUser reqUser, CreateProjectDTO createProjectDTO);

    Task<bool> UpdateProject(ReqUser reqUser, int projectId, UpdateProjectDTO updateProjectDTO);
}

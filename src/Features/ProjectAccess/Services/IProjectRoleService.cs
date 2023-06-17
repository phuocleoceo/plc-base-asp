using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Services;

public interface IProjectRoleService
{
    Task<List<ProjectRoleDTO>> GetAllProjectRoles();

    Task<PagedList<ProjectRoleDTO>> GetProjectRoles(ProjectRoleParams roleParams);

    Task<ProjectRoleDTO> GetProjectRoleById(int projectRoleId);

    Task<bool> CreateProjectRole(CreateProjectRoleDTO createRoleDTO);

    Task<bool> UpdateProjectRole(int projectRoleId, UpdateProjectRoleDTO updateRoleDTO);
}

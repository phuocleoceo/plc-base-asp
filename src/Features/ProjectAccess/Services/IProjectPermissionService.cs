using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.ProjectAccess.Services;

public interface IProjectPermissionService
{
    Task<IEnumerable<ProjectPermissionGroupDTO>> GetForProjectRole(int projectRoleId);

    Task<bool> CreateProjectPermission(
        int projectRoleId,
        CreateProjectPermissionDTO createProjectPermissionDTO
    );

    Task<bool> DeleteProjectPermission(int projectRoleId, string projectPermissionKey);
}

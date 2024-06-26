using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectAccess.Repositories;

public interface IProjectPermissionRepository : IBaseRepository<ProjectPermissionEntity>
{
    Task<List<ProjectPermissionEntity>> GetForProjectRole(int projectRoleId);

    Task<List<ProjectPermissionEntity>> GetForProjectRoles(List<int> projectRoleIds);
}

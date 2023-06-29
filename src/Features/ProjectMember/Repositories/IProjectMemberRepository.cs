using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectMember.Repositories;

public interface IProjectMemberRepository : IBaseRepository<ProjectMemberEntity>
{
    Task<List<int>> GetProjectIdsForUser(int userId);

    Task SoftDeleteMemberForProject(int projectId);

    Task<IEnumerable<string>> GetPermissionsInProjectForUser(int userId, int projectId);
}

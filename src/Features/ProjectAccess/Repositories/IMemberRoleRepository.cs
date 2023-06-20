using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectAccess.Repositories;

public interface IMemberRoleRepository : IBaseRepository<MemberRoleEntity>
{
    Task<List<MemberRoleEntity>> GetByProjectMemberIds(IEnumerable<int> projectMemberIds);
}

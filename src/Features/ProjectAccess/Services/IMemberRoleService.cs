using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Services;

public interface IMemberRoleService
{
    Task<List<MemberRoleDTO>> GetProjectRoleForMember(int projectMemberId);

    Task<bool> CreateMemberRole(CreateMemberRoleDTO createMemberRoleDTO);

    Task<bool> DeleteMemberRole(int projectMemberId, int projectRoleId);
}

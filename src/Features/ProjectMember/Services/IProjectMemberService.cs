using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectMember.Services;

public interface IProjectMemberService
{
    Task<PagedList<ProjectMemberDTO>> GetMembersForProject(
        int projectId,
        ProjectMemberParams projectMemberParams
    );

    Task<List<ProjectMemberSelectDTO>> GetMembersForSelect(int projectId);

    Task<bool> DeleteProjectMember(int projectId, int projectMemberId);
}

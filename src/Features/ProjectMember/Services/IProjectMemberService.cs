using PlcBase.Features.ProjectMember.DTOs;

namespace PlcBase.Features.ProjectMember.Services;

public interface IProjectMemberService
{
    Task<List<ProjectMemberDTO>> GetMembersForProject(
        int projectId,
        ProjectMemberParams projectMemberParams
    );

    Task<bool> DeleteProjectMember(int projectId, int projectMemberId);
}

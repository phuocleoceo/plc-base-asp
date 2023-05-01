namespace PlcBase.Features.ProjectMember.Services;

public interface IProjectMemberService
{
    Task<bool> DeleteProjectMember(int projectId, int projectMemberId);
}

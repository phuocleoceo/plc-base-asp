using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Base.DTO;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.ProjectMember.Services;

public interface IProjectMemberService
{
    Task<PagedList<ProjectMemberDTO>> GetMembersForProject(
        int projectId,
        ProjectMemberParams projectMemberParams
    );

    Task<List<ProjectMemberSelectDTO>> GetMembersForSelect(int projectId);

    Task<bool> DeleteProjectMember(int projectId, int projectMemberId);

    Task<bool> LeaveProject(ReqUser reqUser, int projectId);
}

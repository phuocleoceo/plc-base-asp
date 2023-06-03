using PlcBase.Features.Invitation.DTOs;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Issue.Services;

public interface IIssueService
{
    Task<IEnumerable<IssueBoardGroupDTO>> GetIssuesForBoard(
        int projectId,
        IssueBoardParams issueParams
    );

    Task<List<IssueBacklogDTO>> GetIssuesInBacklog(int projectId, IssueBacklogParams issueParams);

    Task<IssueDetailDTO> GetIssueById(int projectId, int issueId);

    Task<bool> CreateIssue(ReqUser reqUser, int projectId, CreateIssueDTO createIssueDTO);

    Task<bool> UpdateIssue(
        ReqUser reqUser,
        int projectId,
        int issueId,
        UpdateIssueDTO updateIssueDTO
    );

    Task<bool> DeleteIssue(ReqUser reqUser, int projectId, int issueId);
}

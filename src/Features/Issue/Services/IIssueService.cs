using PlcBase.Features.Issue.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Issue.Services;

public interface IIssueService
{
    Task<List<IssueDTO>> GetIssuesForBoard(int projectId);

    Task<List<IssueDTO>> GetIssuesInBacklog(int projectId);

    Task<List<IssueDTO>> GetIssuesInSprint(int projectId);

    Task<bool> CreateIssue(ReqUser reqUser, int projectId, CreateIssueDTO createIssueDTO);

    Task<bool> UpdateIssue(
        ReqUser reqUser,
        int projectId,
        int issueId,
        UpdateIssueDTO updateIssueDTO
    );

    Task<bool> DeleteIssue(ReqUser reqUser, int projectId, int issueId);
}

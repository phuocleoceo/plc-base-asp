using PlcBase.Features.Issue.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Issue.Services;

public interface IIssueService
{
    #region Board
    Task<IEnumerable<IssueBoardGroupDTO>> GetIssuesForBoard(
        int projectId,
        int sprintId,
        IssueBoardParams issueParams
    );

    Task<bool> UpdateBoardIssue(
        int projectId,
        int issueId,
        UpdateBoardIssueDTO updateBoardIssueDTO
    );
    #endregion

    #region Backlog
    Task<List<IssueBacklogDTO>> GetIssuesInBacklog(int projectId, IssueBacklogParams issueParams);

    Task<bool> UpdateBacklogIssue(
        int projectId,
        int issueId,
        UpdateBacklogIssueDTO updateBacklogIssueDTO
    );
    #endregion

    #region Detail
    Task<IssueDetailDTO> GetIssueById(int projectId, int issueId);

    Task<bool> CreateIssue(ReqUser reqUser, int projectId, CreateIssueDTO createIssueDTO);

    Task<bool> UpdateIssue(
        ReqUser reqUser,
        int projectId,
        int issueId,
        UpdateIssueDTO updateIssueDTO
    );

    Task<bool> DeleteIssue(ReqUser reqUser, int projectId, int issueId);
    #endregion
}

using PlcBase.Features.Issue.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Issue.Services;

public interface IIssueCommentService
{
    Task<List<IssueCommentDTO>> GetCommentsForIssue(int issueId);

    Task<bool> CreateIssueComment(
        ReqUser reqUser,
        int issueId,
        CreateIssueCommentDTO createIssueCommentDTO
    );

    Task<bool> UpdateIssueComment(
        ReqUser reqUser,
        int issueId,
        int commentId,
        UpdateIssueCommentDTO updateIssueCommentDTO
    );

    Task<bool> DeleteIssueComment(ReqUser reqUser, int issueId, int commentId);
}

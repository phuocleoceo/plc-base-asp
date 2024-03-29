using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Issue.Services;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Issue.Controllers;

public class IssueCommentController : BaseController
{
    private readonly IIssueCommentService _issueCommentService;

    public IssueCommentController(IIssueCommentService issueCommentService)
    {
        _issueCommentService = issueCommentService;
    }

    [HttpGet("/api/issue/{issueId}/comment")]
    [Authorize]
    public async Task<SuccessResponse<PagedList<IssueCommentDTO>>> GetCommentsForIssue(
        int issueId,
        [FromQuery] IssueCommentParams issueCommentParams
    )
    {
        return HttpContext.Success(
            await _issueCommentService.GetCommentsForIssue(issueId, issueCommentParams)
        );
    }

    [HttpPost("/api/issue/{issueId}/comment")]
    [Authorize]
    public async Task<SuccessResponse<bool>> CreateIssueComment(
        int issueId,
        [FromBody] CreateIssueCommentDTO createIssueCommentDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueCommentService.CreateIssueComment(reqUser, issueId, createIssueCommentDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/issue/{issueId}/comment/{commentId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> UpdateIssueComment(
        int issueId,
        int commentId,
        [FromBody] UpdateIssueCommentDTO updateIssueCommentDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (
            await _issueCommentService.UpdateIssueComment(
                reqUser,
                issueId,
                commentId,
                updateIssueCommentDTO
            )
        )
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/issue/{issueId}/comment/{commentId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeleteIssueComment(int issueId, int commentId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueCommentService.DeleteIssueComment(reqUser, issueId, commentId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

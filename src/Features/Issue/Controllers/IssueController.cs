using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Issue.Services;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Issue.Controllers;

public class IssueController : BaseController
{
    private readonly IIssueService _issueService;

    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    // Board
    [HttpGet("/api/project/{projectId}/board/{sprintId}/issue")]
    [Authorize]
    public async Task<SuccessResponse<IEnumerable<IssueBoardGroupDTO>>> GetIssuesForBoard(
        int projectId,
        int sprintId,
        [FromQuery] IssueBoardParams issueParams
    )
    {
        return HttpContext.Success(
            await _issueService.GetIssuesForBoard(projectId, sprintId, issueParams)
        );
    }

    [HttpPut("/api/project/{projectId}/board/issue/{issueId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> UpdateBoardIssue(
        int projectId,
        int issueId,
        [FromBody] UpdateBoardIssueDTO updateBoardIssueDTO
    )
    {
        if (await _issueService.UpdateBoardIssue(projectId, issueId, updateBoardIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/board/issue/move-to-backlog")]
    [Authorize]
    public async Task<SuccessResponse<bool>> MoveBoardIssueToBacklog(
        int projectId,
        [FromBody] MoveIssueDTO moveIssueDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueService.MoveIssueToBacklog(reqUser, projectId, moveIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    // Backlog
    [HttpGet("/api/project/{projectId}/backlog/issue")]
    [Authorize]
    public async Task<SuccessResponse<List<IssueBacklogDTO>>> GetIssuesInBacklog(
        int projectId,
        [FromQuery] IssueBacklogParams issueParams
    )
    {
        return HttpContext.Success(await _issueService.GetIssuesInBacklog(projectId, issueParams));
    }

    [HttpPut("/api/project/{projectId}/backlog/issue/{issueId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> UpdateBacklogIssue(
        int projectId,
        int issueId,
        [FromBody] UpdateBacklogIssueDTO updateBacklogIssueDTO
    )
    {
        if (await _issueService.UpdateBacklogIssue(projectId, issueId, updateBacklogIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/backlog/issue/move-to-sprint")]
    [Authorize]
    public async Task<SuccessResponse<bool>> MoveBacklogIssueToSprint(
        int projectId,
        [FromBody] MoveIssueDTO moveIssueDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueService.MoveIssueToSprint(reqUser, projectId, moveIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    // Detail
    [HttpGet("/api/project/{projectId}/issue/{issueId}")]
    [Authorize]
    public async Task<SuccessResponse<IssueDetailDTO>> GetIssueById(int projectId, int issueId)
    {
        return HttpContext.Success(await _issueService.GetIssueById(projectId, issueId));
    }

    [HttpPost("/api/project/{projectId}/issue")]
    public async Task<SuccessResponse<bool>> CreateIssue(
        int projectId,
        [FromBody] CreateIssueDTO createIssueDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueService.CreateIssue(reqUser, projectId, createIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/issue/{issueId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> UpdateIssue(
        int projectId,
        int issueId,
        [FromBody] UpdateIssueDTO updateIssueDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueService.UpdateIssue(reqUser, projectId, issueId, updateIssueDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/project/{projectId}/issue/{issueId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeleteIssue(int projectId, int issueId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _issueService.DeleteIssue(reqUser, projectId, issueId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

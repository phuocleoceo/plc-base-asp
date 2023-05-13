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

    [HttpGet("/api/project/{projectId}/backlog/issue")]
    public async Task<BaseResponse<List<IssueDTO>>> GetIssuesInBacklog(int projectId)
    {
        return HttpContext.Success(await _issueService.GetIssuesInBacklog(projectId));
    }

    [HttpGet("/api/project/{projectId}/sprint/{sprintId}/issue")]
    public async Task<BaseResponse<List<IssueDTO>>> GetIssuesInSprint(int projectId, int sprintId)
    {
        return HttpContext.Success(await _issueService.GetIssuesInSprint(projectId, sprintId));
    }

    [HttpPost("/api/project/{projectId}/issue")]
    public async Task<BaseResponse<bool>> CreateIssue(
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
    public async Task<BaseResponse<bool>> UpdateIssue(
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
}

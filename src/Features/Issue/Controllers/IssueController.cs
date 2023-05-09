using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Issue.Services;
using PlcBase.Features.Issue.DTOs;
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
}

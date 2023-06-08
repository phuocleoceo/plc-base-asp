using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Invitation.DTOs;
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
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Invitation.Services;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Invitation.Controllers;

public class InvitationController : BaseController
{
    private readonly IInvitationService _invitationService;

    public InvitationController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpGet("/api/project/{projectId}/invitation")]
    [Authorize]
    public async Task<SuccessResponse<PagedList<SenderInvitationDTO>>> GetInvitationsForProject(
        int projectId,
        [FromQuery] SenderInvitationParams senderInvitationParams
    )
    {
        return HttpContext.Success(
            await _invitationService.GetInvitationsForProject(projectId, senderInvitationParams)
        );
    }

    [HttpPost("/api/project/{projectId}/invitation")]
    [Authorize]
    public async Task<SuccessResponse<bool>> CreateInvitation(
        int projectId,
        [FromBody] CreateInvitationDTO createInvitationDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _invitationService.CreateInvitaion(reqUser, projectId, createInvitationDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/project/{projectId}/invitation/{invitationId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeleteInvitation(int projectId, int invitationId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _invitationService.DeleteInvitation(reqUser, projectId, invitationId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpGet("/api/user/personal/invitation")]
    [Authorize]
    public async Task<SuccessResponse<PagedList<RecipientInvitationDTO>>> GetInvitationsForUser(
        [FromQuery] RecipientInvitationParams recipientInvitationParams
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(
            await _invitationService.GetInvitationsForUser(reqUser, recipientInvitationParams)
        );
    }

    [HttpPut("/api/user/personal/invitation/{invitationId}/accept")]
    [Authorize]
    public async Task<SuccessResponse<bool>> AcceptInvitation(int invitationId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _invitationService.AcceptInvitation(reqUser, invitationId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/user/personal/invitation/{invitationId}/decline")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeclineInvitation(int invitationId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _invitationService.DeclineInvitation(reqUser, invitationId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

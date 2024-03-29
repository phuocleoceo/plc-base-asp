using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Controllers;

[Route("api/member-role")]
public class MemberRoleController : BaseController
{
    private readonly IMemberRoleService _memberRoleService;

    public MemberRoleController(IMemberRoleService memberRoleService)
    {
        _memberRoleService = memberRoleService;
    }

    [HttpGet("{projectMemberId}")]
    [Authorize]
    public async Task<SuccessResponse<List<MemberRoleDTO>>> GetProjectRoleForMember(
        int projectMemberId
    )
    {
        return HttpContext.Success(
            await _memberRoleService.GetProjectRoleForMember(projectMemberId)
        );
    }

    [HttpPost("")]
    [Authorize]
    public async Task<SuccessResponse<bool>> CreateMemberRole(
        [FromBody] CreateMemberRoleDTO createMemberRoleDTO
    )
    {
        if (await _memberRoleService.CreateMemberRole(createMemberRoleDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeleteMemberRole(
        int projectMemberId,
        int projectRoleId
    )
    {
        if (await _memberRoleService.DeleteMemberRole(projectMemberId, projectRoleId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

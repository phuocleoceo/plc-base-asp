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
    public async Task<BaseResponse<List<MemberRoleDTO>>> GetProjectRoleForMember(
        int projectMemberId
    )
    {
        return HttpContext.Success(
            await _memberRoleService.GetProjectRoleForMember(projectMemberId)
        );
    }

    [HttpPost("")]
    public async Task<BaseResponse<bool>> CreateMemberRole(
        [FromBody] CreateMemberRoleDTO createMemberRoleDTO
    )
    {
        if (await _memberRoleService.CreateMemberRole(createMemberRoleDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("")]
    public async Task<BaseResponse<bool>> DeleteMemberRole(int projectMemberId, int projectRoleId)
    {
        if (await _memberRoleService.DeleteMemberRole(projectMemberId, projectRoleId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

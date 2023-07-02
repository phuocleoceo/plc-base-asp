using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectMember.Services;
using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectMember.Controllers;

public class ProjectMemberController : BaseController
{
    private readonly IProjectMemberService _projectMemberService;

    public ProjectMemberController(IProjectMemberService projectMemberService)
    {
        _projectMemberService = projectMemberService;
    }

    [HttpGet("/api/project/{projectId}/member")]
    [Authorize]
    public async Task<BaseResponse<PagedList<ProjectMemberDTO>>> GetMemberForProject(
        int projectId,
        [FromQuery] ProjectMemberParams projectMemberParams
    )
    {
        return HttpContext.Success(
            await _projectMemberService.GetMembersForProject(projectId, projectMemberParams)
        );
    }

    [HttpGet("/api/project/{projectId}/member/select")]
    [Authorize]
    public async Task<BaseResponse<List<ProjectMemberSelectDTO>>> GetMemberForSelect(int projectId)
    {
        return HttpContext.Success(await _projectMemberService.GetMembersForSelect(projectId));
    }

    [HttpDelete("/api/project/{projectId}/member/{projectMemberId}")]
    [Authorize]
    public async Task<BaseResponse<bool>> DeleteProjectMember(int projectId, int projectMemberId)
    {
        if (await _projectMemberService.DeleteProjectMember(projectId, projectMemberId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/member/leave")]
    [Authorize]
    public async Task<BaseResponse<bool>> LeaveProject(int projectId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _projectMemberService.LeaveProject(reqUser, projectId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectMember.Services;
using PlcBase.Features.ProjectMember.DTOs;
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
    public async Task<BaseResponse<List<ProjectMemberDTO>>> GetMemberForProject(
        int projectId,
        [FromQuery] ProjectMemberParams projectMemberParams
    )
    {
        return HttpContext.Success(
            await _projectMemberService.GetMembersForProject(projectId, projectMemberParams)
        );
    }

    [HttpDelete("/api/project/{projectId}/member/{projectMemberId}")]
    public async Task<BaseResponse<bool>> DeleteProjectMember(int projectId, int projectMemberId)
    {
        if (await _projectMemberService.DeleteProjectMember(projectId, projectMemberId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

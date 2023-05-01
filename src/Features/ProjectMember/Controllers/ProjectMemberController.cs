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

    [HttpDelete("/api/project/{projectId}/member/{projectMemberId}")]
    public async Task<BaseResponse<bool>> DeleteProjectMember(int projectId, int projectMemberId)
    {
        if (await _projectMemberService.DeleteProjectMember(projectId, projectMemberId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

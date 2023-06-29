using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Controllers;

[Route("/api/project-role/{projectRoleId}/project-permission")]
public class ProjectPermissionController : BaseController
{
    private readonly IProjectPermissionService _projectPermissionService;

    public ProjectPermissionController(IProjectPermissionService projectPermissionService)
    {
        _projectPermissionService = projectPermissionService;
    }

    [HttpGet("")]
    public async Task<BaseResponse<List<ProjectPermissionDTO>>> GetForProjectRole(int projectRoleId)
    {
        return HttpContext.Success(
            await _projectPermissionService.GetForProjectRole(projectRoleId)
        );
    }

    [HttpPost("")]
    public async Task<BaseResponse<bool>> CreateProjectPermission(
        int projectRoleId,
        [FromBody] CreateProjectPermissionDTO createProjectPermissionDTO
    )
    {
        if (
            await _projectPermissionService.CreateProjectPermission(
                projectRoleId,
                createProjectPermissionDTO
            )
        )
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("{projectPermissionKey}")]
    public async Task<BaseResponse<bool>> DeleteProjectPermission(
        int projectRoleId,
        string projectPermissionKey
    )
    {
        if (
            await _projectPermissionService.DeleteProjectPermission(
                projectRoleId,
                projectPermissionKey
            )
        )
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

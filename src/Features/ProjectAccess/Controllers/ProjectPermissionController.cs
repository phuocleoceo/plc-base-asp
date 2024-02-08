using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Shared.Enums;
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
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<SuccessResponse<IEnumerable<ProjectPermissionGroupDTO>>> GetForProjectRole(
        int projectRoleId
    )
    {
        return HttpContext.Success(
            await _projectPermissionService.GetForProjectRole(projectRoleId)
        );
    }

    [HttpPost("")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<SuccessResponse<bool>> CreateProjectPermission(
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
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<SuccessResponse<bool>> DeleteProjectPermission(
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Shared.Enums;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Controllers;

[Route("/api/project-role")]
public class ProjectRoleController : BaseController
{
    private readonly IProjectRoleService _projectRoleService;

    public ProjectRoleController(IProjectRoleService projectRoleService)
    {
        _projectRoleService = projectRoleService;
    }

    [HttpGet("")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<PagedList<ProjectRoleDTO>>> GetProjectRoles(
        [FromQuery] ProjectRoleParams roleParams
    )
    {
        return HttpContext.Success(await _projectRoleService.GetProjectRoles(roleParams));
    }

    [HttpGet("all")]
    [Authorize]
    public async Task<BaseResponse<List<ProjectRoleDTO>>> GetAllProjectRoles()
    {
        return HttpContext.Success(await _projectRoleService.GetAllProjectRoles());
    }

    [HttpGet("{projectRoleId}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<ProjectRoleDTO>> GetProjectRoleById(int projectRoleId)
    {
        return HttpContext.Success(await _projectRoleService.GetProjectRoleById(projectRoleId));
    }

    [HttpPost("")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<bool>> CreateProjectRole(
        [FromBody] CreateProjectRoleDTO createProjectRoleDTO
    )
    {
        if (await _projectRoleService.CreateProjectRole(createProjectRoleDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("{projectRoleId}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<bool>> UpdateProjectRole(
        int projectRoleId,
        [FromBody] UpdateProjectRoleDTO updateProjectRoleDTO
    )
    {
        if (await _projectRoleService.UpdateProjectRole(projectRoleId, updateProjectRoleDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("{projectRoleId}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<bool>> DeleteProjectRole(int projectRoleId)
    {
        if (await _projectRoleService.DeleteProjectRole(projectRoleId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

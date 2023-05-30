using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectStatus.Services;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectStatus.Controllers;

public class ProjectStatusController : BaseController
{
    private readonly IProjectStatusService _projectStatusService;

    public ProjectStatusController(IProjectStatusService projectStatusService)
    {
        _projectStatusService = projectStatusService;
    }

    [HttpGet("/api/project/{projectId}/status")]
    public async Task<BaseResponse<List<ProjectStatusDTO>>> GetProjectStatusForProject(
        int projectId
    )
    {
        return HttpContext.Success(
            await _projectStatusService.GetProjectStatusForProject(projectId)
        );
    }

    [HttpPost("/api/project/{projectId}/status")]
    public async Task<BaseResponse<bool>> CreateProjectStatus(
        int projectId,
        [FromBody] CreateProjectStatusDTO createProjectStatusDTO
    )
    {
        if (await _projectStatusService.CreateProjectStatus(projectId, createProjectStatusDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/status/{projectStatusId}")]
    public async Task<BaseResponse<bool>> UpdateProjectStatus(
        int projectId,
        int projectStatusId,
        [FromBody] UpdateProjectStatusDTO updateProjectStatusDTO
    )
    {
        if (
            await _projectStatusService.UpdateProjectStatus(
                projectId,
                projectStatusId,
                updateProjectStatusDTO
            )
        )
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/project/{projectId}/status/{projectStatusId}")]
    public async Task<BaseResponse<bool>> DeleteProjectStatus(int projectId, int projectStatusId)
    {
        if (await _projectStatusService.DeleteProjectStatus(projectId, projectStatusId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

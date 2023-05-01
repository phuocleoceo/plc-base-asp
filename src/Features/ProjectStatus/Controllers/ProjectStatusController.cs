using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectStatus.Services;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
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
}

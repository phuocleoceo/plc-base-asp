using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Project.Services;
using PlcBase.Features.Project.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Project.Controllers;

public class ProjectController : BaseController
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [Authorize]
    public async Task<BaseResponse<PagedList<ProjectDTO>>> GetProjectsForUser(
        [FromQuery] ProjectParams projectParams
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(
            await _projectService.GetProjectsForUser(reqUser, projectParams)
        );
    }

    [HttpGet("{projectId}")]
    [Authorize]
    public async Task<BaseResponse<ProjectDTO>> GetProjectById(int projectId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(await _projectService.GetProjectById(reqUser, projectId));
    }

    [HttpGet("{projectId}/permission")]
    [Authorize]
    public async Task<BaseResponse<IEnumerable<string>>> GetPermissionsInProjectForUser(
        int projectId
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(
            await _projectService.GetPermissionsInProjectForUser(reqUser, projectId)
        );
    }

    [HttpPost]
    [Authorize]
    public async Task<BaseResponse<bool>> CreateProject(
        [FromBody] CreateProjectDTO createProjectDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _projectService.CreateProject(reqUser, createProjectDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("{projectId}")]
    [Authorize]
    public async Task<BaseResponse<bool>> UpdateProject(
        int projectId,
        [FromBody] UpdateProjectDTO updateProjectDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _projectService.UpdateProject(reqUser, projectId, updateProjectDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("{projectId}")]
    [Authorize]
    public async Task<BaseResponse<bool>> DeleteProject(int projectId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _projectService.DeleteProject(reqUser, projectId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

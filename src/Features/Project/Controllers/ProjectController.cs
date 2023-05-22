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
    public async Task<BaseResponse<List<ProjectDTO>>> GetProjectsForUser(
        [FromQuery] ProjectParams projectParams
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(
            await _projectService.GetProjectsForUser(reqUser, projectParams)
        );
    }

    [HttpGet("{projectId}")]
    public async Task<BaseResponse<ProjectDTO>> GetProjectById(int projectId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        return HttpContext.Success(await _projectService.GetProjectById(reqUser, projectId));
    }

    [HttpPost]
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
    public async Task<BaseResponse<bool>> DeleteProject(int projectId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _projectService.DeleteProject(reqUser, projectId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

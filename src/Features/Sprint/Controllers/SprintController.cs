using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Sprint.Services;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Sprint.Controllers;

public class SprintController : BaseController
{
    private readonly ISprintService _sprintService;

    public SprintController(ISprintService sprintService)
    {
        _sprintService = sprintService;
    }

    [HttpGet("/api/project/{projectId}/sprint")]
    [Authorize]
    public async Task<SuccessResponse<SprintDTO>> GetAvailableSprint(int projectId)
    {
        return HttpContext.Success(await _sprintService.GetAvailableSprint(projectId));
    }

    [HttpGet("/api/project/{projectId}/sprint/{sprintId}")]
    [Authorize]
    public async Task<SuccessResponse<SprintDTO>> GetSprintById(int projectId, int sprintId)
    {
        return HttpContext.Success(await _sprintService.GetSprintById(projectId, sprintId));
    }

    [HttpPost("/api/project/{projectId}/sprint")]
    [Authorize]
    public async Task<SuccessResponse<bool>> CreateSprint(
        int projectId,
        [FromBody] CreateSprintDTO createSprintDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.CreateSprint(reqUser, projectId, createSprintDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/sprint/{sprintId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> UpdateSprint(
        int projectId,
        int sprintId,
        [FromBody] UpdateSprintDTO updateSprintDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.UpdateSprint(reqUser, projectId, sprintId, updateSprintDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/project/{projectId}/sprint/{sprintId}")]
    [Authorize]
    public async Task<SuccessResponse<bool>> DeleteSprint(int projectId, int sprintId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.DeleteSprint(reqUser, projectId, sprintId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/sprint/{sprintId}/start")]
    [Authorize]
    public async Task<SuccessResponse<bool>> StartSprint(int projectId, int sprintId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.StartSprint(reqUser, projectId, sprintId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/sprint/{sprintId}/complete")]
    [Authorize]
    public async Task<SuccessResponse<bool>> CompleteSprint(
        int projectId,
        int sprintId,
        [FromBody] CompleteSprintDTO completeSprintDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.CompleteSprint(reqUser, projectId, sprintId, completeSprintDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

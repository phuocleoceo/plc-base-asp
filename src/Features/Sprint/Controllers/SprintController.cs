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

    [HttpPost("/api/project/{projectId}/sprint")]
    public async Task<BaseResponse<bool>> CreateSprint(
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
    public async Task<BaseResponse<bool>> UpdateSprint(
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
    public async Task<BaseResponse<bool>> DeleteSprint(int projectId, int sprintId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.DeleteSprint(reqUser, projectId, sprintId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/sprint/{sprintId}/start")]
    public async Task<BaseResponse<bool>> StartSprint(int projectId, int sprintId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.StartSprint(reqUser, projectId, sprintId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("/api/project/{projectId}/sprint/{sprintId}/complete")]
    public async Task<BaseResponse<bool>> CompleteSprint(int projectId, int sprintId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _sprintService.CompleteSprint(reqUser, projectId, sprintId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

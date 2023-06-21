using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Event.Services;
using PlcBase.Features.Event.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Event.Controllers;

public class EventController : BaseController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost("/api/project/{projectId}/event")]
    public async Task<BaseResponse<bool>> CreateEvent(
        int projectId,
        [FromBody] CreateEventDTO createEventDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _eventService.CreateEvent(reqUser, projectId, createEventDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

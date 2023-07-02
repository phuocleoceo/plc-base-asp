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

    [HttpGet("/api/project/{projectId}/event")]
    [Authorize]
    public async Task<BaseResponse<List<EventDTO>>> GetEvents(
        int projectId,
        [FromQuery] EventParams eventParams
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();
        return HttpContext.Success(await _eventService.GetEvents(reqUser, projectId, eventParams));
    }

    [HttpGet("/api/project/{projectId}/event/{eventId}")]
    [Authorize]
    public async Task<BaseResponse<EventDetailDTO>> GetEventDetail(int projectId, int eventId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();
        return HttpContext.Success(await _eventService.GetEventDetail(reqUser, projectId, eventId));
    }

    [HttpPost("/api/project/{projectId}/event")]
    [Authorize]
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

    [HttpPut("/api/project/{projectId}/event/{eventId}")]
    [Authorize]
    public async Task<BaseResponse<bool>> UpdateEvent(
        int projectId,
        int eventId,
        [FromBody] UpdateEventDTO updateEventDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _eventService.UpdateEvent(reqUser, projectId, eventId, updateEventDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpDelete("/api/project/{projectId}/event/{eventId}")]
    [Authorize]
    public async Task<BaseResponse<bool>> DeleteEvent(int projectId, int eventId)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _eventService.DeleteEvent(reqUser, projectId, eventId))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Event.Services;
using PlcBase.Features.Event.DTOs;
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
}

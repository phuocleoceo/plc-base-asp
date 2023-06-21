using PlcBase.Features.Event.DTOs;
using PlcBase.Base.DomainModel;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Event.Services;

public interface IEventService
{
    Task<bool> CreateEvent(ReqUser reqUser, int projectId, CreateEventDTO createEventDTO);
}

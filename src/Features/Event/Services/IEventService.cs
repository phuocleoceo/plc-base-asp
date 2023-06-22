using PlcBase.Features.Event.DTOs;
using PlcBase.Base.DomainModel;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Event.Services;

public interface IEventService
{
    Task<List<EventDTO>> GetEvents(ReqUser reqUser, int projectId, EventParams eventParams);

    Task<EventDetailDTO> GetEventDetail(ReqUser reqUser, int projectId, int eventId);

    Task<bool> CreateEvent(ReqUser reqUser, int projectId, CreateEventDTO createEventDTO);

    Task<bool> UpdateEvent(
        ReqUser reqUser,
        int projectId,
        int eventId,
        UpdateEventDTO updateEventDTO
    );

    Task<bool> DeleteEvent(ReqUser reqUser, int projectId, int eventId);
}

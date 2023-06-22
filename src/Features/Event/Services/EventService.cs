using AutoMapper;

using PlcBase.Features.Event.Entities;
using PlcBase.Features.Event.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Event.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EventService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<EventDTO>> GetEvents(
        ReqUser reqUser,
        int projectId,
        EventParams eventParams
    )
    {
        QueryModel<EventEntity> eventQuery = new QueryModel<EventEntity>()
        {
            Includes = { e => e.Attendees },
            Filters =
            {
                e => e.ProjectId == projectId,
                e => e.Attendees.Select(a => a.UserId).Contains(reqUser.Id),
                e =>
                    (e.StartTime.Month == eventParams.Month && e.StartTime.Year == eventParams.Year)
                    || (e.EndTime.Month == eventParams.Month && e.EndTime.Year == eventParams.Year)
            },
        };

        return await _uow.Event.GetManyAsync<EventDTO>(eventQuery);
    }

    public Task<EventDetailDTO> GetEventDetail(ReqUser reqUser, int projectId, int eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateEvent(
        ReqUser reqUser,
        int projectId,
        CreateEventDTO createEventDTO
    )
    {
        try
        {
            await _uow.CreateTransaction();

            EventEntity eventEntity = _mapper.Map<EventEntity>(createEventDTO);

            eventEntity.CreatorId = reqUser.Id;
            eventEntity.ProjectId = projectId;
            _uow.Event.Add(eventEntity);
            await _uow.Save();

            IEnumerable<EventAttendeeEntity> eventAttendees = createEventDTO.AttendeeIds.Select(
                ea => new EventAttendeeEntity() { UserId = ea, EventId = eventEntity.Id }
            );

            _uow.EventAttendee.AddRange(eventAttendees);
            await _uow.Save();

            await _uow.CommitTransaction();
            return true;
        }
        catch (Exception ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public Task<bool> UpdateEvent(
        ReqUser reqUser,
        int projectId,
        int eventId,
        UpdateEventDTO updateEventDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEvent(ReqUser reqUser, int projectId, int eventId)
    {
        throw new NotImplementedException();
    }
}

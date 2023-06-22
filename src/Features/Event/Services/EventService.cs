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

    public async Task<EventDetailDTO> GetEventDetail(ReqUser reqUser, int projectId, int eventId)
    {
        return await _uow.Event.GetOneAsync<EventDetailDTO>(
            new QueryModel<EventEntity>()
            {
                Filters =
                {
                    e => e.Id == eventId && e.ProjectId == projectId && e.CreatorId == reqUser.Id
                }
            }
        );
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
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public async Task<bool> UpdateEvent(
        ReqUser reqUser,
        int projectId,
        int eventId,
        UpdateEventDTO updateEventDTO
    )
    {
        try
        {
            await _uow.CreateTransaction();

            EventEntity eventDb = await _uow.Event.GetForUpdateAndDelete(
                reqUser.Id,
                projectId,
                eventId
            );

            if (eventDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "event_not_found");

            _mapper.Map(updateEventDTO, eventDb);
            _uow.Event.Update(eventDb);

            HashSet<int> currentAttendees = await _uow.EventAttendee.GetAttendeeIdsForEvent(
                eventId
            );

            // Những userId Db không có, update data có => thêm mới
            IEnumerable<int> createAttendees = updateEventDTO.AttendeeIds.Except(currentAttendees);
            // Những userId DB có, update data không có => gỡ đi
            IEnumerable<int> removeAttendees = currentAttendees.Except(updateEventDTO.AttendeeIds);
            // Những userId cả Db và update data có thì giữ nguyên

            _uow.EventAttendee.AddRange(
                createAttendees.Select(
                    attendeeId =>
                        new EventAttendeeEntity() { UserId = attendeeId, EventId = eventId }
                )
            );

            await _uow.EventAttendee.RemoveAttendeesByUserIds(removeAttendees);

            await _uow.Save();
            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public async Task<bool> DeleteEvent(ReqUser reqUser, int projectId, int eventId)
    {
        EventEntity eventDb = await _uow.Event.GetForUpdateAndDelete(
            reqUser.Id,
            projectId,
            eventId
        );

        if (eventDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "event_not_found");

        _uow.Event.Remove(eventDb);
        return await _uow.Save();
    }
}

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
}

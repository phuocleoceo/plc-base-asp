using AutoMapper;

using PlcBase.Features.Event.Entities;

namespace PlcBase.Features.Event.DTOs;

public class EventMapping : Profile
{
    public EventMapping()
    {
        CreateMap<EventEntity, EventDTO>();

        CreateMap<EventEntity, EventDetailDTO>();

        CreateMap<CreateEventDTO, EventEntity>();

        CreateMap<UpdateEventDTO, EventEntity>();
    }
}

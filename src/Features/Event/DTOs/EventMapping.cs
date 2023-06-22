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

        CreateMap<EventAttendeeEntity, EventAttendeeDTO>()
            .ForMember(dto => dto.Id, prop => prop.MapFrom(entity => entity.User.Id))
            .ForMember(dto => dto.Email, prop => prop.MapFrom(entity => entity.User.Email))
            .ForMember(
                dto => dto.Name,
                prop => prop.MapFrom(entity => entity.User.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.Avatar,
                prop => prop.MapFrom(entity => entity.User.UserProfile.Avatar)
            )
            .ForMember(dto => dto.AttendeeId, prop => prop.MapFrom(entity => entity.Id));
    }
}

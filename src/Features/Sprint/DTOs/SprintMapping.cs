using AutoMapper;

using PlcBase.Features.Sprint.Entities;

namespace PlcBase.Features.Sprint.DTOs;

public class SprintMapping : Profile
{
    public SprintMapping()
    {
        CreateMap<SprintEntity, SprintDTO>();

        CreateMap<CreateSprintDTO, SprintEntity>();

        CreateMap<UpdateSprintDTO, SprintEntity>();
    }
}

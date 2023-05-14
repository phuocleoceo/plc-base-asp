using AutoMapper;

using PlcBase.Features.Sprint.Entities;

namespace PlcBase.Features.Sprint.DTOs;

public class SprintMapping : Profile
{
    public SprintMapping()
    {
        CreateMap<CreateSprintDTO, SprintEntity>();

        CreateMap<UpdateSprintDTO, SprintEntity>();
    }
}

using AutoMapper;

using PlcBase.Features.Media.Entities;

namespace PlcBase.Features.Media.DTOs;

public class MediaMapping : Profile
{
    public MediaMapping()
    {
        CreateMap<CreateMediaDTO, MediaEntity>();

        CreateMap<MediaEntity, MediaDTO>();
    }
}

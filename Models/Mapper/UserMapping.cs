using PlcBase.Models.Entities;
using PlcBase.Models.DTO;
using AutoMapper;

namespace PlcBase.Models.Mapper;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserRegisterDTO, UserAccountEntity>();

        CreateMap<UserRegisterDTO, UserProfileEntity>();
    }
}
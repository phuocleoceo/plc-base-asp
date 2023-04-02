using PlcBase.Features.User.Entities;
using AutoMapper;

namespace PlcBase.Features.Auth.DTOs;

public class AuthMapping : Profile
{
    public AuthMapping()
    {
        CreateMap<UserRegisterDTO, UserAccountEntity>();

        CreateMap<UserRegisterDTO, UserProfileEntity>();
    }
}
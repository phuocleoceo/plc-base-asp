using AutoMapper;

using PlcBase.Features.User.Entities;

namespace PlcBase.Features.Auth.DTOs;

public class AuthMapping : Profile
{
    public AuthMapping()
    {
        CreateMap<UserRegisterDTO, UserAccountEntity>();

        CreateMap<UserRegisterDTO, UserProfileEntity>();
    }
}
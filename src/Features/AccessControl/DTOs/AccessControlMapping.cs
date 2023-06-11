using AutoMapper;

using PlcBase.Features.AccessControl.Entities;

namespace PlcBase.Features.AccessControl.DTOs;

public class AccessControlMapping : Profile
{
    public AccessControlMapping()
    {
        CreateMap<RoleEntity, RoleDTO>();
    }
}

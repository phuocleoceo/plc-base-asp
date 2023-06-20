using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Features.ProjectAccess.DTOs;

public class MemberRoleMapping : Profile
{
    public MemberRoleMapping()
    {
        CreateMap<MemberRoleEntity, MemberRoleDTO>();

        CreateMap<CreateMemberRoleDTO, MemberRoleEntity>();
    }
}

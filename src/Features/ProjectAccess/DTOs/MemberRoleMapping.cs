using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Features.ProjectAccess.DTOs;

public class MemberRoleMapping : Profile
{
    public MemberRoleMapping()
    {
        CreateMap<MemberRoleEntity, MemberRoleDTO>();

        CreateMap<MemberRoleEntity, String>().ConvertUsing(entity => entity.ProjectRole.Name);

        CreateMap<CreateMemberRoleDTO, MemberRoleEntity>();
    }
}

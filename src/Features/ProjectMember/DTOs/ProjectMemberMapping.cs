using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;

namespace PlcBase.Features.ProjectMember.DTOs;

public class ProjectMemberMapping : Profile
{
    public ProjectMemberMapping()
    {
        CreateMap<ProjectMemberEntity, ProjectMemberDTO>()
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
            .ForMember(dto => dto.ProjectMemberId, prop => prop.MapFrom(entity => entity.Id));
    }
}

using AutoMapper;

using PlcBase.Features.Project.Entities;

namespace PlcBase.Features.Project.DTOs;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<ProjectEntity, ProjectDTO>()
            .ForMember(
                dto => dto.LeaderName,
                prop => prop.MapFrom(entity => entity.Leader.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.LeaderAvatar,
                prop => prop.MapFrom(entity => entity.Leader.UserProfile.Avatar)
            );

        CreateMap<CreateProjectDTO, ProjectEntity>();

        CreateMap<UpdateProjectDTO, ProjectEntity>();
    }
}

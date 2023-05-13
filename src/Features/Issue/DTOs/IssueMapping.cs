using AutoMapper;

using PlcBase.Features.Issue.Entities;

namespace PlcBase.Features.Issue.DTOs;

public class IssueMapping : Profile
{
    public IssueMapping()
    {
        CreateMap<IssueEntity, IssueDTO>()
            .ForMember(
                dto => dto.ReporterName,
                prop => prop.MapFrom(entity => entity.Reporter.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.ReporterAvatar,
                prop => prop.MapFrom(entity => entity.Reporter.UserProfile.Avatar)
            )
            .ForMember(
                dto => dto.AssigneeName,
                prop => prop.MapFrom(entity => entity.Assignee.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.AssigneeAvatar,
                prop => prop.MapFrom(entity => entity.Assignee.UserProfile.Avatar)
            )
            .ForMember(
                dto => dto.ProjectStatusName,
                prop => prop.MapFrom(entity => entity.ProjectStatus.Name)
            );

        CreateMap<CreateIssueDTO, IssueEntity>();

        CreateMap<UpdateIssueDTO, IssueEntity>();
    }
}

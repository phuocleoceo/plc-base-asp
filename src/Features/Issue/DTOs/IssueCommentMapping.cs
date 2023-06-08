using AutoMapper;

using PlcBase.Features.Issue.Entities;

namespace PlcBase.Features.Issue.DTOs;

public class IssueCommentMapping : Profile
{
    public IssueCommentMapping()
    {
        CreateMap<IssueCommentEntity, IssueCommentDTO>()
            .ForMember(
                dto => dto.UserName,
                prop => prop.MapFrom(entity => entity.User.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.UserAvatar,
                prop => prop.MapFrom(entity => entity.User.UserProfile.Avatar)
            );
        ;

        CreateMap<CreateIssueCommentDTO, IssueCommentEntity>();

        CreateMap<UpdateIssueCommentDTO, IssueCommentEntity>();
    }
}

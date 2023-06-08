using AutoMapper;

using PlcBase.Features.Issue.Entities;

namespace PlcBase.Features.Issue.DTOs;

public class IssueCommentMapping : Profile
{
    public IssueCommentMapping()
    {
        CreateMap<IssueCommentEntity, IssueCommentDTO>();

        CreateMap<CreateIssueCommentDTO, IssueCommentEntity>();

        CreateMap<UpdateIssueCommentDTO, IssueCommentEntity>();
    }
}

using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

namespace PlcBase.Features.Issue.Services;

public class IssueCommentService : IIssueCommentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public IssueCommentService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
}

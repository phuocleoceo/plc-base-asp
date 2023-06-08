using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

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

    public async Task<PagedList<IssueCommentDTO>> GetCommentsForIssue(
        int issueId,
        IssueCommentParams issueCommentParams
    )
    {
        return await _uow.IssueComment.GetPagedAsync<IssueCommentDTO>(
            new QueryModel<IssueCommentEntity>()
            {
                OrderBy = c => c.OrderByDescending(c => c.CreatedAt),
                Filters = { c => c.Id == issueId },
                PageSize = issueCommentParams.PageSize,
                PageNumber = issueCommentParams.PageNumber
            }
        );
    }

    public Task<bool> CreateIssueComment(
        ReqUser reqUser,
        int issueId,
        CreateIssueCommentDTO createIssueCommentDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateIssueComment(
        ReqUser reqUser,
        int issueId,
        int commentId,
        UpdateIssueCommentDTO updateIssueCommentDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteIssueComment(ReqUser reqUser, int issueId, int commentId)
    {
        throw new NotImplementedException();
    }
}

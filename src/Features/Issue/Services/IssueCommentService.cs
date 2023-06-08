using AutoMapper;

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
                OrderBy = p => p.OrderByDescending(c => c.CreatedAt),
                Filters = { c => c.IssueId == issueId },
                Includes = { c => c.User.UserProfile },
                PageSize = issueCommentParams.PageSize,
                PageNumber = issueCommentParams.PageNumber
            }
        );
    }

    public async Task<bool> CreateIssueComment(
        ReqUser reqUser,
        int issueId,
        CreateIssueCommentDTO createIssueCommentDTO
    )
    {
        IssueCommentEntity issueCommentEntity = _mapper.Map<IssueCommentEntity>(
            createIssueCommentDTO
        );

        issueCommentEntity.UserId = reqUser.Id;
        issueCommentEntity.IssueId = issueId;

        _uow.IssueComment.Add(issueCommentEntity);
        return await _uow.Save();
    }

    public async Task<bool> UpdateIssueComment(
        ReqUser reqUser,
        int issueId,
        int commentId,
        UpdateIssueCommentDTO updateIssueCommentDTO
    )
    {
        IssueCommentEntity issueCommentDb = await _uow.IssueComment.GetForUpdateAndDelete(
            reqUser.Id,
            issueId,
            commentId
        );

        if (issueCommentDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "issue_comment_not_found");

        _mapper.Map(updateIssueCommentDTO, issueCommentDb);
        _uow.IssueComment.Update(issueCommentDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteIssueComment(ReqUser reqUser, int issueId, int commentId)
    {
        IssueCommentEntity issueCommentDb = await _uow.IssueComment.GetForUpdateAndDelete(
            reqUser.Id,
            issueId,
            commentId
        );

        if (issueCommentDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "issue_comment_not_found");

        _uow.IssueComment.Remove(issueCommentDb);
        return await _uow.Save();
    }
}

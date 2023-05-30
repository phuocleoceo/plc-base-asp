using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

namespace PlcBase.Features.Issue.Services;

public class IssueService : IIssueService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public IssueService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<Dictionary<int, List<IssueBoardDTO>>> GetIssuesForBoard(int projectId)
    {
        SprintEntity sprintEntity = await _uow.Sprint.GetInProgressSprint(projectId);

        if (sprintEntity == null)
            throw new BaseException(HttpCode.NOT_FOUND, "no_sprint_in_progress");

        return (
            await _uow.Issue.GetManyAsync<IssueBoardDTO>(
                new QueryModel<IssueEntity>()
                {
                    OrderBy = c => c.OrderBy(i => i.ProjectStatusIndex),
                    Filters =
                    {
                        i =>
                            i.ProjectId == projectId
                            && i.DeletedAt == null
                            && i.SprintId == sprintEntity.Id
                            && i.BacklogIndex == null
                    },
                    Includes = { i => i.Assignee.UserProfile, i => i.Reporter.UserProfile, },
                }
            )
        ).GroupBy(i => i.ProjectStatusId.Value).ToDictionary(ig => ig.Key, ig => ig.ToList());
    }

    public async Task<List<IssueDTO>> GetIssuesInBacklog(int projectId)
    {
        return await _uow.Issue.GetManyAsync<IssueDTO>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderBy(i => i.BacklogIndex),
                Filters =
                {
                    i =>
                        i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.BacklogIndex != null
                        && i.SprintId == null
                },
                Includes =
                {
                    i => i.Assignee.UserProfile,
                    i => i.Reporter.UserProfile,
                    i => i.ProjectStatus
                },
            }
        );
    }

    public async Task<List<IssueDTO>> GetIssuesInSprint(int projectId)
    {
        SprintEntity sprintEntity = await _uow.Sprint.GetInProgressSprint(projectId);

        if (sprintEntity == null)
            throw new BaseException(HttpCode.NOT_FOUND, "no_sprint_in_progress");

        return await _uow.Issue.GetManyAsync<IssueDTO>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderByDescending(i => i.CreatedAt),
                Filters =
                {
                    i =>
                        i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.SprintId == sprintEntity.Id
                        && i.BacklogIndex == null
                },
                Includes =
                {
                    i => i.Assignee.UserProfile,
                    i => i.Reporter.UserProfile,
                    i => i.ProjectStatus
                },
            }
        );
    }

    public async Task<bool> CreateIssue(
        ReqUser reqUser,
        int projectId,
        CreateIssueDTO createIssueDTO
    )
    {
        IssueEntity issueEntity = _mapper.Map<IssueEntity>(createIssueDTO);

        issueEntity.ReporterId = reqUser.Id;
        issueEntity.ProjectId = projectId;
        issueEntity.ProjectStatusId = await _uow.ProjectStatus.GetStatusIdForNewIssue(projectId);
        issueEntity.ProjectStatusIndex = Math.Floor(
            _uow.Issue.GetStatusIndexForNewIssue(projectId, issueEntity.ProjectStatusId.Value)
        );
        issueEntity.BacklogIndex = Math.Floor(_uow.Issue.GetBacklogIndexForNewIssue(projectId));

        _uow.Issue.Add(issueEntity);
        return await _uow.Save();
    }

    public async Task<bool> UpdateIssue(
        ReqUser reqUser,
        int projectId,
        int issueId,
        UpdateIssueDTO updateIssueDTO
    )
    {
        IssueEntity issueDb = await _uow.Issue.GetForUpdateAndDelete(
            projectId,
            reqUser.Id,
            issueId
        );

        if (issueDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "issue_not_found");

        _mapper.Map(updateIssueDTO, issueDb);
        _uow.Issue.Update(issueDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteIssue(ReqUser reqUser, int projectId, int issueId)
    {
        IssueEntity issueDb = await _uow.Issue.GetForUpdateAndDelete(
            projectId,
            reqUser.Id,
            issueId
        );

        if (issueDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "issue_not_found");

        _uow.Issue.SoftDelete(issueDb);
        return await _uow.Save();
    }
}

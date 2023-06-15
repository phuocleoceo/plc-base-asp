using AutoMapper;

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

    #region Board
    public async Task<IEnumerable<IssueBoardGroupDTO>> GetIssuesForBoard(
        int projectId,
        int sprintId,
        IssueBoardParams issueParams
    )
    {
        QueryModel<IssueEntity> issueQuery = new QueryModel<IssueEntity>()
        {
            OrderBy = c => c.OrderBy(i => i.ProjectStatusIndex),
            Filters =
            {
                i =>
                    i.ProjectId == projectId
                    && i.DeletedAt == null
                    && i.SprintId == sprintId
                    && i.BacklogIndex == null
            },
            Includes = { i => i.Assignee.UserProfile, },
        };

        if (!String.IsNullOrEmpty(issueParams.Assignees))
        {
            IEnumerable<int> assignees = issueParams.Assignees
                .Split(",")
                .Select(c => Convert.ToInt32(c));
            issueQuery.Filters.Add(i => assignees.Contains(i.AssigneeId.Value));
        }

        if (!string.IsNullOrWhiteSpace(issueParams.SearchValue))
        {
            string searchValue = issueParams.SearchValue.ToLower();
            issueQuery.Filters.Add(
                i =>
                    i.Title.ToLower().Contains(searchValue)
                    || i.StoryPoint.ToString().ToLower().Contains(searchValue)
            );
        }

        return (await _uow.Issue.GetManyAsync<IssueBoardDTO>(issueQuery))
            .GroupBy(i => i.ProjectStatusId.Value)
            .Select(
                ig =>
                    new IssueBoardGroupDTO()
                    {
                        ProjectStatusId = ig.Key,
                        Issues = ig.AsEnumerable()
                    }
            );
    }

    public async Task<bool> UpdateBoardIssue(
        int projectId,
        int issueId,
        UpdateBoardIssueDTO updateBoardIssueDTO
    )
    {
        IssueEntity issueDb = await _uow.Issue.GetOneAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                Filters =
                {
                    i =>
                        i.Id == issueId
                        && i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.BacklogIndex == null
                },
            }
        );

        if (issueDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "board_issue_not_found");

        _mapper.Map(updateBoardIssueDTO, issueDb);
        _uow.Issue.Update(issueDb);
        return await _uow.Save();
    }

    public async Task<bool> MoveBoardIssueToBacklog(
        ReqUser reqUser,
        int projectId,
        MoveIssueDTO moveIssueDTO
    )
    {
        List<IssueEntity> issues = await _uow.Issue.GetByIds(moveIssueDTO.Issues);
        double backlogIndex = _uow.Issue.GetBacklogIndexForNewIssue(projectId);
        issues.ForEach(i =>
        {
            i.SprintId = null;
            i.BacklogIndex = backlogIndex++;
            _uow.Issue.Update(i);
        });

        return await _uow.Save();
    }
    #endregion

    #region Backlog
    public async Task<List<IssueBacklogDTO>> GetIssuesInBacklog(
        int projectId,
        IssueBacklogParams issueParams
    )
    {
        QueryModel<IssueEntity> issueQuery = new QueryModel<IssueEntity>()
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
            Includes = { i => i.Assignee.UserProfile, },
        };

        if (!String.IsNullOrEmpty(issueParams.Assignees))
        {
            IEnumerable<int> assignees = issueParams.Assignees
                .Split(",")
                .Select(c => Convert.ToInt32(c));
            issueQuery.Filters.Add(i => assignees.Contains(i.AssigneeId.Value));
        }

        if (!string.IsNullOrWhiteSpace(issueParams.SearchValue))
        {
            string searchValue = issueParams.SearchValue.ToLower();
            issueQuery.Filters.Add(
                i =>
                    i.Title.ToLower().Contains(searchValue)
                    || i.StoryPoint.ToString().ToLower().Contains(searchValue)
            );
        }

        return await _uow.Issue.GetManyAsync<IssueBacklogDTO>(issueQuery);
    }

    public async Task<bool> UpdateBacklogIssue(
        int projectId,
        int issueId,
        UpdateBacklogIssueDTO updateBacklogIssueDTO
    )
    {
        IssueEntity issueDb = await _uow.Issue.GetOneAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                Filters =
                {
                    i =>
                        i.Id == issueId
                        && i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.BacklogIndex != null
                        && i.SprintId == null
                },
            }
        );

        if (issueDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "backlog_issue_not_found");

        _mapper.Map(updateBacklogIssueDTO, issueDb);
        _uow.Issue.Update(issueDb);
        return await _uow.Save();
    }

    public async Task<bool> MoveBacklogIssueToSprint(
        ReqUser reqUser,
        int projectId,
        MoveIssueDTO moveIssueDTO
    )
    {
        SprintEntity availableSprint = await _uow.Sprint.GetAvailableSprint(projectId);

        if (availableSprint == null)
            throw new BaseException(HttpCode.NOT_FOUND, "no_available_sprint");

        List<IssueEntity> issues = await _uow.Issue.GetByIds(moveIssueDTO.Issues);
        issues.ForEach(i =>
        {
            i.SprintId = availableSprint.Id;
            i.BacklogIndex = null;
            _uow.Issue.Update(i);
        });

        return await _uow.Save();
    }

    #endregion

    #region Detail
    public async Task<IssueDetailDTO> GetIssueById(int projectId, int issueId)
    {
        return await _uow.Issue.GetOneAsync<IssueDetailDTO>(
            new QueryModel<IssueEntity>()
            {
                Filters =
                {
                    i => i.Id == issueId && i.ProjectId == projectId && i.DeletedAt == null
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
    #endregion
}

using AutoMapper;

using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Issue.Repositories;

public class IssueRepository : BaseRepository<IssueEntity>, IIssueRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public IssueRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public double GetBacklogIndexForNewIssue(int projectId)
    {
        return _dbSet
                .Where(
                    i =>
                        i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.BacklogIndex.HasValue
                        && !i.SprintId.HasValue
                )
                .Max(i => i.BacklogIndex) + 1
            ?? 0;
    }

    public double GetStatusIndexForNewIssue(int projectId, int projectStatusId)
    {
        return _dbSet
                .Where(i => i.ProjectId == projectId && i.ProjectStatusId == projectStatusId)
                .Max(i => i.ProjectStatusIndex) + 1
            ?? 0;
    }

    public async Task<IssueEntity> GetForUpdateAndDelete(int projectId, int reporterId, int issueId)
    {
        return await GetOneAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                Filters =
                {
                    i =>
                        i.Id == issueId
                        && i.ProjectId == projectId
                        && i.ReporterId == reporterId
                        && i.DeletedAt == null
                },
            }
        );
    }

    public async Task<List<IssueEntity>> GetByIds(List<int> ids)
    {
        return await GetManyAsync<IssueEntity>(
            new QueryModel<IssueEntity>() { Filters = { i => ids.Contains(i.Id) } }
        );
    }

    public async Task MoveIssueToBacklog(List<int> issueIds, int projectId)
    {
        List<IssueEntity> issues = await GetByIds(issueIds);

        double backlogIndex = GetBacklogIndexForNewIssue(projectId);
        issues.ForEach(i =>
        {
            i.SprintId = null;
            i.BacklogIndex = backlogIndex++;
            Update(i);
        });
    }

    public async Task MoveIssueFromSprintToBacklog(int sprintId, int projectId)
    {
        List<IssueEntity> issues = await GetManyAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderBy(i => i.ProjectStatusId).ThenBy(i => i.ProjectStatusIndex),
                Filters =
                {
                    i =>
                        i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.SprintId == sprintId
                        && i.BacklogIndex == null
                }
            }
        );

        double backlogIndex = GetBacklogIndexForNewIssue(projectId);
        issues.ForEach(i =>
        {
            i.SprintId = null;
            i.BacklogIndex = backlogIndex++;
            Update(i);
        });
    }

    public async Task MoveIssueToSprint(List<int> issueIds, int sprintId)
    {
        List<IssueEntity> issues = await GetByIds(issueIds);

        issues.ForEach(i =>
        {
            i.SprintId = sprintId;
            i.BacklogIndex = null;
            Update(i);
        });
    }
}

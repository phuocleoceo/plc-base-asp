using AutoMapper;

using PlcBase.Features.Issue.Entities;
using Microsoft.EntityFrameworkCore;
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
                .Where(i => i.ProjectId == projectId && i.BacklogIndex.HasValue)
                .Max(i => i.BacklogIndex) + 1
            ?? 0;
    }

    public async Task<IssueEntity> GetForUpdateAndDelete(int projectId, int reporterId, int issueId)
    {
        return await GetOneAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                Filters =
                {
                    i => i.Id == issueId && i.ProjectId == projectId && i.ReporterId == reporterId
                },
            }
        );
    }

    public async Task<Dictionary<int, List<IssueEntity>>> GetIssuesGroupedByStatus(
        int projectId,
        int sprintId
    )
    {
        return (
            await _dbSet
                .Include(i => i.Reporter.UserProfile)
                .Include(i => i.Assignee.UserProfile)
                .Where(
                    i =>
                        i.ProjectId == projectId
                        && i.DeletedAt == null
                        && i.SprintId == sprintId
                        && i.BacklogIndex == null
                )
                .ToListAsync()
        )
            .GroupBy(i => i.ProjectStatusId.Value)
            .ToDictionary(ig => ig.Key, ig => ig.ToList());
    }
}

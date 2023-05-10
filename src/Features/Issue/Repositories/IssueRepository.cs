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

    public async Task<int> GetBacklogIndexForNewIssue(int projectId)
    {
        IssueEntity issue = await GetOneAsync<IssueEntity>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderByDescending(i => i.BacklogIndex),
                Filters = { s => s.ProjectId == projectId }
            }
        );

        return issue?.BacklogIndex + 1 ?? 0;
    }
}

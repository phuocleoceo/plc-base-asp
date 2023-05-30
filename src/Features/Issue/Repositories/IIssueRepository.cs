using PlcBase.Features.Issue.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Issue.Repositories;

public interface IIssueRepository : IBaseRepository<IssueEntity>
{
    double GetBacklogIndexForNewIssue(int projectId);

    Task<IssueEntity> GetForUpdateAndDelete(int projectId, int reporterId, int issueId);

    Task<Dictionary<int, List<IssueEntity>>> GetIssuesGroupedByStatus(int projectId, int sprintId);
}

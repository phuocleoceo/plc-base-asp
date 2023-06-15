using PlcBase.Features.Issue.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Issue.Repositories;

public interface IIssueRepository : IBaseRepository<IssueEntity>
{
    double GetBacklogIndexForNewIssue(int projectId);

    double GetStatusIndexForNewIssue(int projectId, int projectStatusId);

    Task<IssueEntity> GetForUpdateAndDelete(int projectId, int reporterId, int issueId);

    Task<List<IssueEntity>> GetByIds(List<int> ids);

    Task MoveIssueToBacklog(List<int> issueIds, int projectId);

    Task MoveIssueToSprint(List<int> issueIds, int sprintId);
}

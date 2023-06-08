using PlcBase.Features.Issue.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Issue.Repositories;

public interface IIssueCommentRepository : IBaseRepository<IssueCommentEntity>
{
    Task<IssueCommentEntity> GetForUpdateAndDelete(int userId, int issueId);
}

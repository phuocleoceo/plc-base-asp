using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectStatus.Repositories;

public interface IProjectStatusRepository : IBaseRepository<ProjectStatusEntity>
{
    Task<int> GetIndexForNewStatus(int projectId);

    Task<int?> GetStatusIdForNewIssue(int projectId);
}

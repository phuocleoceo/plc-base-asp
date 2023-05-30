using PlcBase.Features.Sprint.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Sprint.Repositories;

public interface ISprintRepository : IBaseRepository<SprintEntity>
{
    Task<SprintEntity> GetForUpdateAndDelete(int projectId, int sprintId);

    Task<SprintEntity> GetInProgressSprint(int projectId);
}

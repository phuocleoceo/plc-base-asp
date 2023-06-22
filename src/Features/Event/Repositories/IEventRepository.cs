using PlcBase.Features.Event.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Event.Repositories;

public interface IEventRepository : IBaseRepository<EventEntity>
{
    Task<EventEntity> GetForUpdateAndDelete(int creatorId, int projectId, int eventId);
}

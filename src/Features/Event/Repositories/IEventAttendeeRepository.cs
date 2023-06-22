using PlcBase.Features.Event.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Event.Repositories;

public interface IEventAttendeeRepository : IBaseRepository<EventAttendeeEntity>
{
    Task<HashSet<int>> GetAttendeeIdsForEvent(int eventId);

    Task RemoveAttendeesByUserIds(IEnumerable<int> userIds);
}

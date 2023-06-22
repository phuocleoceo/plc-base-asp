using Microsoft.EntityFrameworkCore;
using AutoMapper;

using PlcBase.Features.Event.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Event.Repositories;

public class EventAttendeeRepository : BaseRepository<EventAttendeeEntity>, IEventAttendeeRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public EventAttendeeRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<HashSet<int>> GetAttendeeIdsForEvent(int eventId)
    {
        return (
            await _dbSet.Where(ea => ea.EventId == eventId).Select(ea => ea.UserId).ToListAsync()
        ).ToHashSet();
    }

    public async Task RemoveAttendeesByUserIds(IEnumerable<int> userIds)
    {
        IEnumerable<EventAttendeeEntity> attendeeEntities = await _dbSet
            .Where(ea => userIds.Contains(ea.UserId))
            .ToListAsync();

        RemoveRange(attendeeEntities);
    }
}

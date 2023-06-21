using Microsoft.EntityFrameworkCore;
using AutoMapper;

using PlcBase.Features.Event.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Event.Repositories;

public class EventRepository : BaseRepository<EventEntity>, IEventRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public EventRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}

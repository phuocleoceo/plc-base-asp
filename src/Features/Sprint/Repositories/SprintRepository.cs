using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Sprint.Repositories;

public class SprintRepository : BaseRepository<SprintEntity>, ISprintRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public SprintRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}

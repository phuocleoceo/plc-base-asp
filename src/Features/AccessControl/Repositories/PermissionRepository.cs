using AutoMapper;

using PlcBase.Features.AccessControl.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.AccessControl.Repositories;

public class PermissionRepository : BaseRepository<PermissionEntity>, IPermisisonRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public PermissionRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}

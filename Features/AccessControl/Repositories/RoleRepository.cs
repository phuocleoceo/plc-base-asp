using AutoMapper;

using PlcBase.Features.AccessControl.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.AccessControl.Repositories;

public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public RoleRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
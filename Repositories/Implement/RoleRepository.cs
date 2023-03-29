using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Repositories.Implement;

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
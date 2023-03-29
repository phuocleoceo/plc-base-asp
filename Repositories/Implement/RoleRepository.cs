using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{
    private readonly DataContext _db;

    public RoleRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
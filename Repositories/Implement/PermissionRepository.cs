using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class PermissionRepository : BaseRepository<PermissionEntity>, IPermisisonRepository
{
    private readonly DataContext _db;

    public PermissionRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
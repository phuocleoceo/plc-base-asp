using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class AddressWardRepository : BaseRepository<AddressWardEntity>, IAddressWardRepository
{
    private readonly DataContext _db;

    public AddressWardRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
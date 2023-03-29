using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class AddressDistrictRepository : BaseRepository<AddressDistrictEntity>, IAddressDistrictRepository
{
    private readonly DataContext _db;

    public AddressDistrictRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
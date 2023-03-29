using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class AddressProvinceRepository : BaseRepository<AddressProvinceEntity>, IAddressProvinceRepository
{
    private readonly DataContext _db;

    public AddressProvinceRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
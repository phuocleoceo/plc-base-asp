using PlcBase.Repositories.Interface;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _db;

    public UnitOfWork(DataContext db)
    {
        _db = db;

        AddressProvince = new AddressProvinceRepository(_db);
        AddressDistrict = new AddressDistrictRepository(_db);
        AddressWard = new AddressWardRepository(_db);
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task<int> Save()
    {
        return await _db.SaveChangesAsync();
    }

    public IAddressProvinceRepository AddressProvince { get; private set; }
    public IAddressDistrictRepository AddressDistrict { get; private set; }
    public IAddressWardRepository AddressWard { get; private set; }
}
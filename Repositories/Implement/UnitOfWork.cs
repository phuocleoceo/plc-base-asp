using PlcBase.Repositories.Interface;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Repositories.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public UnitOfWork(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;

        AddressProvince = new AddressProvinceRepository(_db, mapper);
        AddressDistrict = new AddressDistrictRepository(_db, mapper);
        AddressWard = new AddressWardRepository(_db, mapper);
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
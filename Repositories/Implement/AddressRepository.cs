using PlcBase.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class AddressRepository : BaseRepository<AddressProvinceEntity>, IAddressRepository
{
    private readonly DataContext _db;

    public AddressRepository(DataContext db) : base(db)
    {
        _db = db;
    }

    public async Task<List<AddressProvinceEntity>> GetProvinces()
    {
        return await _db.AddressProvinces.ToListAsync();
    }

    public async Task<AddressProvinceEntity> GetProvince(int id)
    {
        return await _db.AddressProvinces.Include(e => e.AddressDistricts)
                                         .FirstOrDefaultAsync(c => c.Id == id);

    }

    public async Task<List<AddressDistrictEntity>> GetDistricts()
    {
        return await _db.AddressDistricts.ToListAsync();
    }

    public async Task<AddressDistrictEntity> GetDistrict(int id)
    {
        return await _db.AddressDistricts.Include(p => p.AddressWards)
                                         .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<AddressWardEntity> GetFullAddressByWardId(int wardId)
    {
        return await _db.AddressWards.Include(p => p.AddressDistrict.AddressProvince)
                                     .FirstOrDefaultAsync(p => p.Id == wardId);
    }
}
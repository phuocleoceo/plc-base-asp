using PlcBase.Repositories.Interface;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcBase.Services.Interface;
using PlcBase.Models.DTO;
using AutoMapper;

namespace PlcBase.Services.Implement;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public AddressService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<List<ProvinceDTO>> GetProvinces()
    {
        return await _uof.AddressProvince.GetQuery()
                        .ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    public async Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId)
    {
        return await _uof.AddressDistrict.GetQuery(d => d.AddressProvinceId == provinceId)
                        .ProjectTo<DistrictDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    public async Task<List<WardDTO>> GetWardsOfDistrict(int districtId)
    {
        return await _uof.AddressWard.GetQuery(w => w.AddressDistrictId == districtId)
                        .ProjectTo<WardDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    public async Task<FullAddressDTO> GetFullAddressByWardId(int wardId)
    {
        return await _uof.AddressWard.GetQuery(w => w.Id == wardId)
                        .Include(w => w.AddressDistrict.AddressProvince)
                        .ProjectTo<FullAddressDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
}
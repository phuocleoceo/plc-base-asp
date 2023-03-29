using PlcBase.Repositories.Interface;
using PlcBase.Services.Interface;
using PlcBase.Base.DomainModel;
using PlcBase.Models.Entities;
using PlcBase.Models.DTO;
using AutoMapper;

namespace PlcBase.Services.Implement;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _uof;

    public AddressService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
    }

    public async Task<List<ProvinceDTO>> GetProvinces()
    {
        return await _uof.AddressProvince.GetManyAsync<ProvinceDTO>();
    }

    public async Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId)
    {
        return await _uof.AddressDistrict.GetManyAsync<DistrictDTO>(
            new QueryModel<AddressDistrictEntity>
            {
                Filters = { d => d.AddressProvinceId == provinceId },
            }
        );
    }

    public async Task<List<WardDTO>> GetWardsOfDistrict(int districtId)
    {
        return await _uof.AddressWard.GetManyAsync<WardDTO>(
            new QueryModel<AddressWardEntity>
            {
                Filters = { w => w.AddressDistrictId == districtId },
            }
        );
    }

    public async Task<FullAddressDTO> GetFullAddressByWardId(int wardId)
    {
        return await _uof.AddressWard.GetOneAsync<FullAddressDTO>(
            new QueryModel<AddressWardEntity>
            {
                Filters = { w => w.Id == wardId },
                Includes = { w => w.AddressDistrict.AddressProvince },
            }
        );
    }
}
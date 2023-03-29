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
        return await _uof.AddressProvince.GetAllAsync<ProvinceDTO>();
    }

    public async Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId)
    {
        QueryModel<AddressDistrictEntity> queryModel = new QueryModel<AddressDistrictEntity>
        {
            Filters = { d => d.AddressProvinceId == provinceId },
        };
        return await _uof.AddressDistrict.GetAllAsync<DistrictDTO>(queryModel);
    }

    public async Task<List<WardDTO>> GetWardsOfDistrict(int districtId)
    {
        QueryModel<AddressWardEntity> queryModel = new QueryModel<AddressWardEntity>
        {
            Filters = { w => w.AddressDistrictId == districtId },
        };
        return await _uof.AddressWard.GetAllAsync<WardDTO>(queryModel);
    }

    public async Task<FullAddressDTO> GetFullAddressByWardId(int wardId)
    {
        QueryModel<AddressWardEntity> queryModel = new QueryModel<AddressWardEntity>
        {
            Filters = { w => w.Id == wardId },
            Includes = { w => w.AddressDistrict.AddressProvince },
        };
        return await _uof.AddressWard.GetOneAsync<FullAddressDTO>(queryModel);
    }
}
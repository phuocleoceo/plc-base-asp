using AutoMapper;

using PlcBase.Features.Address.Entities;
using PlcBase.Features.Address.DTO;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Address.Services;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _uow;

    public AddressService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
    }

    public async Task<List<ProvinceDTO>> GetProvinces()
    {
        return await _uow.AddressProvince.GetManyAsync<ProvinceDTO>();
    }

    public async Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId)
    {
        return await _uow.AddressDistrict.GetManyAsync<DistrictDTO>(
            new QueryModel<AddressDistrictEntity> { Filters = { d => d.AddressProvinceId == provinceId }, }
        );
    }

    public async Task<List<WardDTO>> GetWardsOfDistrict(int districtId)
    {
        return await _uow.AddressWard.GetManyAsync<WardDTO>(
            new QueryModel<AddressWardEntity> { Filters = { w => w.AddressDistrictId == districtId }, }
        );
    }

    public async Task<FullAddressDTO> GetFullAddressByWardId(int wardId)
    {
        return await _uow.AddressWard.GetOneAsync<FullAddressDTO>(
            new QueryModel<AddressWardEntity>
            {
                Filters = { w => w.Id == wardId },
                Includes = { w => w.AddressDistrict.AddressProvince },
            }
        );
    }
}

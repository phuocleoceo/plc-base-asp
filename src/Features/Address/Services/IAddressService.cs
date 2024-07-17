using PlcBase.Features.Address.DTOs;

namespace PlcBase.Features.Address.Services;

public interface IAddressService
{
    Task<List<ProvinceDTO>> GetProvinces();

    Task<List<DistrictDTO>> GetDistrictsOfProvince(int provinceId);

    Task<List<WardDTO>> GetWardsOfDistrict(int districtId);

    Task<FullAddressDTO> GetFullAddressByWardId(int wardId);
}

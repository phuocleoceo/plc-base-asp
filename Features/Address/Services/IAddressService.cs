using PlcBase.Features.Address.DTO;

namespace PlcBase.Features.Address.Services;

public interface IAddressService
{
    Task<List<ProvinceDTO>> GetProvinces();

    Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId);

    Task<List<WardDTO>> GetWardsOfDistrict(int districtId);

    Task<FullAddressDTO> GetFullAddressByWardId(int wardId);
}
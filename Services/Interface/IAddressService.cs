using PlcBase.Models.DTO;

namespace PlcBase.Services.Interface;

public interface IAddressService
{
    Task<List<ProvinceDTO>> GetProvinces();

    Task<List<DistrictDTO>> GetDistricsOfProvince(int provinceId);

    Task<List<WardDTO>> GetWardsOfDistrict(int districtId);

    Task<FullAddressDTO> GetFullAddressByWardId(int wardId);
}
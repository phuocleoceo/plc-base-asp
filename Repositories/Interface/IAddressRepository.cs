using PlcBase.Models.Entities;

namespace PlcBase.Repositories.Interface;

public interface IAddressRepository
{
    Task<List<AddressProvinceEntity>> GetProvinces();

    Task<AddressProvinceEntity> GetProvince(int id);

    Task<List<AddressDistrictEntity>> GetDistricts();

    Task<AddressDistrictEntity> GetDistrict(int id);

    Task<AddressWardEntity> GetFullAddressByWardId(int wardId);
}
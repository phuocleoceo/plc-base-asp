using Dapper;

using PlcBase.Features.Address.Entities;
using PlcBase.Features.Address.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

using static PlcBase.Shared.Enums.TableName;

namespace PlcBase.Features.Address.Services;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _uow;

    public AddressService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<ProvinceDTO>> GetProvinces()
    {
        DapperQuery query = new DapperQuery() { Query = $"SELECT * FROM {ADDRESS_PROVINCE}" };
        return await _uow.DapperContainer.QueryAsync<AddressProvinceEntity, ProvinceDTO>(query);
    }

    public async Task<List<DistrictDTO>> GetDistrictsOfProvince(int provinceId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("ProvinceId", provinceId);

        DapperQuery query = new DapperQuery()
        {
            Query =
                @$"SELECT * FROM {ADDRESS_DISTRICT}
                   WHERE province_id = @ProvinceId",
            Params = parameters
        };

        return await _uow.DapperContainer.QueryAsync<AddressDistrictEntity, DistrictDTO>(query);
    }

    public async Task<List<WardDTO>> GetWardsOfDistrict(int districtId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("DistrictId", districtId);

        DapperQuery query = new DapperQuery()
        {
            Query =
                @$"SELECT * FROM {ADDRESS_WARD}
                   WHERE district_id = @DistrictId",
            Params = parameters
        };

        return await _uow.DapperContainer.QueryAsync<AddressWardEntity, WardDTO>(query);
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

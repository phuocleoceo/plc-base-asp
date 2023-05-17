using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Address.Services;
using PlcBase.Features.Address.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Address.Controllers;

public class AddressController : BaseController
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("Provinces")]
    public async Task<BaseResponse<List<ProvinceDTO>>> GetProvinces()
    {
        return HttpContext.Success(await _addressService.GetProvinces());
    }

    [HttpGet("Provinces/{provinceId}/Districts")]
    public async Task<BaseResponse<List<DistrictDTO>>> GetDistricsOfProvince(int provinceId)
    {
        return HttpContext.Success(await _addressService.GetDistricsOfProvince(provinceId));
    }

    [HttpGet("Districts/{districtId}/Wards")]
    public async Task<BaseResponse<List<WardDTO>>> GetWardsOfDistrict(int districtId)
    {
        return HttpContext.Success(await _addressService.GetWardsOfDistrict(districtId));
    }

    [HttpGet("Full-Address/{wardId}")]
    public async Task<BaseResponse<FullAddressDTO>> GetFullAddressByWardId(int wardId)
    {
        return HttpContext.Success(await _addressService.GetFullAddressByWardId(wardId));
    }
}

using PlcBase.Features.Address.Services;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Models.DTO;
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

    [HttpGet("Province/{id}/Districs")]
    public async Task<BaseResponse<List<DistrictDTO>>> GetDistricsOfProvince(int id)
    {
        return HttpContext.Success(await _addressService.GetDistricsOfProvince(id));
    }

    [HttpGet("District/{id}/Wards")]
    public async Task<BaseResponse<List<WardDTO>>> GetWardsOfDistrict(int id)
    {
        return HttpContext.Success(await _addressService.GetWardsOfDistrict(id));
    }

    [HttpGet("Full-Address/{id}")]
    public async Task<BaseResponse<FullAddressDTO>> GetFullAddressByWardId(int id)
    {
        return HttpContext.Success(await _addressService.GetFullAddressByWardId(id));
    }
}
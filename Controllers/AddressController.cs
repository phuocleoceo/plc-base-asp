using PlcBase.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Models.DTO;
using PlcBase.Base.DTO;

namespace PlcBase.Controllers;

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
        List<ProvinceDTO> provinces = await _addressService.GetProvinces();
        return HttpContext.Success(provinces);
    }

    [HttpGet("Province/{id}/Districs")]
    public async Task<BaseResponse<List<DistrictDTO>>> GetDistricsOfProvince(int id)
    {
        List<DistrictDTO> districts = await _addressService.GetDistricsOfProvince(id);
        return HttpContext.Success(districts);
    }

    [HttpGet("District/{id}/Wards")]
    public async Task<BaseResponse<List<WardDTO>>> GetWardsOfDistrict(int id)
    {
        List<WardDTO> wards = await _addressService.GetWardsOfDistrict(id);
        return HttpContext.Success(wards);
    }

    [HttpGet("Full-Address/{id}")]
    public async Task<BaseResponse<FullAddressDTO>> GetFullAddressByWardId(int id)
    {
        FullAddressDTO fullAddress = await _addressService.GetFullAddressByWardId(id);
        return HttpContext.Success(fullAddress);
    }
}
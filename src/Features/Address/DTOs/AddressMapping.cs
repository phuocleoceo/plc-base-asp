using AutoMapper;

using PlcBase.Features.Address.Entities;
using PlcBase.Features.Address.DTO;

namespace PlcBase.Features.Address.DTOs;

public class AddressMapping : Profile
{
    public AddressMapping()
    {
        CreateMap<AddressProvinceEntity, ProvinceDTO>();

        CreateMap<AddressDistrictEntity, DistrictDTO>();

        CreateMap<AddressWardEntity, WardDTO>();

        CreateMap<AddressWardEntity, FullAddressDTO>()
            .ForMember(dto => dto.Ward, prop => prop.MapFrom(entity => entity.Name))
            .ForMember(dto => dto.District, prop => prop.MapFrom(entity => entity.AddressDistrict.Name))
            .ForMember(dto => dto.Province, prop => prop.MapFrom(entity => entity.AddressDistrict.AddressProvince.Name))
            .PreserveReferences();
    }
}

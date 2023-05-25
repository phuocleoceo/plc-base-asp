using AutoMapper;

using PlcBase.Features.User.Entities;

namespace PlcBase.Features.User.DTOs;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserProfileEntity, UserDTO>()
            .ForMember(dto => dto.Email, prop => prop.MapFrom(entity => entity.UserAccount.Email))
            .ForMember(
                dto => dto.AddressWard,
                prop => prop.MapFrom(entity => entity.AddressWard.Name)
            )
            .ForMember(
                dto => dto.AddressDistrict,
                prop => prop.MapFrom(entity => entity.AddressWard.AddressDistrict.Name)
            )
            .ForMember(
                dto => dto.AddressProvince,
                prop =>
                    prop.MapFrom(entity => entity.AddressWard.AddressDistrict.AddressProvince.Name)
            );

        CreateMap<UserProfileEntity, UserProfilePersonalDTO>()
            .ForMember(dto => dto.Email, prop => prop.MapFrom(entity => entity.UserAccount.Email))
            .ForMember(
                dto => dto.AddressWard,
                prop => prop.MapFrom(entity => entity.AddressWard.Name)
            )
            .ForMember(
                dto => dto.AddressDistrictId,
                prop => prop.MapFrom(entity => entity.AddressWard.AddressDistrict.Id)
            )
            .ForMember(
                dto => dto.AddressDistrict,
                prop => prop.MapFrom(entity => entity.AddressWard.AddressDistrict.Name)
            )
            .ForMember(
                dto => dto.AddressProvinceId,
                prop =>
                    prop.MapFrom(entity => entity.AddressWard.AddressDistrict.AddressProvince.Id)
            )
            .ForMember(
                dto => dto.AddressProvince,
                prop =>
                    prop.MapFrom(entity => entity.AddressWard.AddressDistrict.AddressProvince.Name)
            );

        CreateMap<UserProfileEntity, UserProfileAnonymousDTO>()
            .ForMember(dto => dto.Email, prop => prop.MapFrom(entity => entity.UserAccount.Email))
            .ForMember(
                dto => dto.AddressWard,
                prop => prop.MapFrom(entity => entity.AddressWard.Name)
            )
            .ForMember(
                dto => dto.AddressDistrict,
                prop => prop.MapFrom(entity => entity.AddressWard.AddressDistrict.Name)
            )
            .ForMember(
                dto => dto.AddressProvince,
                prop =>
                    prop.MapFrom(entity => entity.AddressWard.AddressDistrict.AddressProvince.Name)
            );

        CreateMap<UserAccountEntity, UserAccountDTO>()
            .ForMember(dto => dto.RoleName, prop => prop.MapFrom(entity => entity.Role.Name));

        CreateMap<UserProfileUpdateDTO, UserProfileEntity>();

        CreateMap<UserAccountUpdateDTO, UserAccountEntity>();
    }
}

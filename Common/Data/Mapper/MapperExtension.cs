using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Features.Address.DTOs;
using PlcBase.Features.Auth.DTOs;

namespace PlcBase.Shared.Data.Mapper;

public static class MapperExtension
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMapping));
        services.AddAutoMapper(typeof(AddressMapping));
        services.AddAutoMapper(typeof(RolePermissionMapping));
    }
}
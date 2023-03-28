using PlcBase.Models.Mapper;

namespace PlcBase.Extensions.ServiceCollections;

public static class MapperExtension
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapping));
        services.AddAutoMapper(typeof(AddressMapping));
        services.AddAutoMapper(typeof(RolePermissionMapping));
    }
}
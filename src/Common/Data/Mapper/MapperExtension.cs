using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Features.Address.DTOs;
using PlcBase.Features.Project.DTOs;
using PlcBase.Features.Media.DTOs;
using PlcBase.Features.Auth.DTOs;

namespace PlcBase.Common.Data.Mapper;

public static class MapperExtension
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMapping));
        services.AddAutoMapper(typeof(AddressMapping));
        services.AddAutoMapper(typeof(RolePermissionMapping));
        services.AddAutoMapper(typeof(ConfigSettingMapping));
        services.AddAutoMapper(typeof(MediaMapping));
        services.AddAutoMapper(typeof(ProjectMapping));
    }
}

using PlcBase.Features.ConfigSetting.Services;
using PlcBase.Features.Address.Services;
using PlcBase.Features.Auth.Services;

namespace PlcBase.Common.Services;

public static class ServiceDIExtension
{
    public static void ConfigureServiceDI(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IConfigSettingService, ConfigSettingService>();
    }
}

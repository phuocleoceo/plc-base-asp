using PlcBase.Services.Interface;
using PlcBase.Services.Implement;

namespace PlcBase.Services;

public static class ServiceDIExtension
{
    public static void ConfigureServiceDI(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
    }
}

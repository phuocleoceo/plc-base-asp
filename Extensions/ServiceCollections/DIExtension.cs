using PlcBase.Models.Mapper;
using PlcBase.Repositories;
using PlcBase.Services;
using PlcBase.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class DIExtension
{
    public static void ConfigureDataFactory(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAutoMapper();
        services.ConfigureRepositoryDI();
        services.ConfigureServiceDI();
        services.ConfigureAppSetting(configuration);
        services.ConfigureHelperDI();
    }
}
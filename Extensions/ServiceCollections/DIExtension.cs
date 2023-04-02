using PlcBase.Models.Mapper;
using PlcBase.Shared.Repositories;
using PlcBase.Shared.Services;
using PlcBase.Shared.Helpers;

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
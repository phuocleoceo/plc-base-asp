using Monolithic.Models.Mapper;
using Monolithic.Helpers;

namespace Monolithic.Extensions.ServiceCollections;

public static class DIExtension
{
    public static void ConfigureDI(this IServiceCollection services)
    {
        services.ConfigureLibraryDI();
        services.ConfigureRepositoryDI();
        services.ConfigureServiceDI();
        services.ConfigureHelperDI();
    }

    private static void ConfigureLibraryDI(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
    }

    private static void ConfigureRepositoryDI(this IServiceCollection services)
    {

    }

    private static void ConfigureServiceDI(this IServiceCollection services)
    {

    }

    private static void ConfigureHelperDI(this IServiceCollection services)
    {
        services.AddScoped<ISendMailHelper, SendMailHelper>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
using PlcBase.Models.Mapper;
using PlcBase.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

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
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IPermissionHelper, PermissionHelper>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddSingleton<IRedisHelper, RedisHelper>();
        services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
        services.AddScoped<IS3Helper, S3Helper>();
        services.AddScoped<IVNPHelper, VNPHelper>();
    }
}
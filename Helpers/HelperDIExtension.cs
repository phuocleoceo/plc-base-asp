namespace PlcBase.Helpers;

public static class HelperDIExtension
{
    public static void ConfigureHelperDI(this IServiceCollection services)
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
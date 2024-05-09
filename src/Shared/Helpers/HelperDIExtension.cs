namespace PlcBase.Shared.Helpers;

public static class HelperDIExtension
{
    public static void ConfigureHelperDI(this IServiceCollection services)
    {
        services.AddSingleton<ISendMailHelper, SendMailHelper>();
        services.AddSingleton<IJwtHelper, JwtHelper>();
        services.AddSingleton<IPermissionHelper, PermissionHelper>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddSingleton<IRedisHelper, RedisHelper>();
        services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
        services.AddSingleton<IS3Helper, S3Helper>();
    }
}

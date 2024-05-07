namespace PlcBase.Shared.Helpers;

public static class AppSettingExtension
{
    public static void ConfigureAppSetting(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<DateTimeSettings>(configuration.GetSection("DateTimeSettings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<VNPSettings>(configuration.GetSection("VNPSettings"));
        services.Configure<AWSSettings>(configuration.GetSection("AWSSettings"));
        services.Configure<ClientAppSettings>(configuration.GetSection("ClientAppSettings"));
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
    }
}

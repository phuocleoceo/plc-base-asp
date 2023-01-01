using PlcBase.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class AppSettingExtension
{
    public static void ConfigureAppSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }
}
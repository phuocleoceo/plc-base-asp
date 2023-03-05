using PlcBase.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class AppSettingExtension
{
    public static void ConfigureAppSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DateTimeSettings>(configuration.GetSection("DateTimeSettings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<S3Settings>(configuration.GetSection("AWSSettings:S3"));
    }
}
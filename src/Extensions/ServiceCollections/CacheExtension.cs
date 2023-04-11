using PlcBase.Shared.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class CacheExtension
{
    public static void ConfigureRedis(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        CacheSettings cacheSettings = configuration
            .GetSection("CacheSettings")
            .Get<CacheSettings>();

        if (cacheSettings.Enable)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettings.ConnectionString;
            });
        }
    }
}

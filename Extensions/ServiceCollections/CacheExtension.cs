using Monolithic.Helpers;

namespace Monolithic.Extensions.ServiceCollections;

public static class CacheExtension
{
    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();

        if (cacheSettings.Enable)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettings.ConnectionString;
            });
        }
    }
}
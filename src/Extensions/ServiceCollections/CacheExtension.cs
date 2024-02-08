using PlcBase.Shared.Helpers;
using StackExchange.Redis;

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

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheSettings.ConnectionString;
        });

        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(cacheSettings.ConnectionString)
        );
    }
}

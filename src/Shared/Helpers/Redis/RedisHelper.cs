using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace PlcBase.Shared.Helpers;

public class RedisHelper : IRedisHelper
{
    private readonly IDistributedCache _redisCache;
    private readonly CacheSettings _cacheSettings;

    public RedisHelper(IDistributedCache redisCache, IOptions<CacheSettings> cacheSettings)
    {
        _redisCache = redisCache;
        _cacheSettings = cacheSettings.Value;
    }

    public async Task Set<T>(string key, T obj)
    {
        string objStr = JsonConvert.SerializeObject(obj);
        await _redisCache.SetStringAsync(key, objStr);
    }

    public async Task SetWithTTL<T>(string key, T obj)
    {
        TimeSpan expires = TimeSpan.FromSeconds(_cacheSettings.Expires);
        DistributedCacheEntryOptions options =
            new DistributedCacheEntryOptions().SetSlidingExpiration(expires);

        string objStr = JsonConvert.SerializeObject(obj);
        await _redisCache.SetStringAsync(key, objStr, options);
    }

    public async Task<T> Get<T>(string key)
    {
        string obj = await _redisCache.GetStringAsync(key);
        return string.IsNullOrEmpty(obj) ? default(T) : JsonConvert.DeserializeObject<T>(obj);
    }

    public async Task Clear(string key)
    {
        await _redisCache.RemoveAsync(key);
    }
}

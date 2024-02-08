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

    public async Task SetWithTtl<T>(string key, T obj)
    {
        // Cache auto expire after
        TimeSpan expires = TimeSpan.FromSeconds(_cacheSettings.Expires);
        // Cache auto expire if not access in
        TimeSpan slidingExpires = TimeSpan.FromSeconds(_cacheSettings.Expires / 2);

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expires,
            SlidingExpiration = slidingExpires
        };

        string objStr = JsonConvert.SerializeObject(obj);
        await _redisCache.SetStringAsync(key, objStr, options);
    }

    public async Task<T> Get<T>(string key)
    {
        string obj = await _redisCache.GetStringAsync(key);
        return string.IsNullOrEmpty(obj) ? default : JsonConvert.DeserializeObject<T>(obj);
    }

    public async Task Clear(string key)
    {
        await _redisCache.RemoveAsync(key);
    }

    public async Task<T> GetCachedOr<T>(string key, Func<T> supplier)
    {
        T cachedData = await Get<T>(key);
        if (cachedData != null)
        {
            return cachedData;
        }

        T data = supplier.Invoke();
        if (data == null)
        {
            return default;
        }

        await SetWithTtl(key, data);
        return data;
    }

    public async Task<List<T>> GetListCachedOr<T>(string key, Func<List<T>> supplier)
    {
        return await GetCachedOr(key, supplier);
    }
}

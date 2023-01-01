using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Monolithic.Helpers;

public class RedisHelper : IRedisHelper
{
    private readonly IDistributedCache _redisCache;
    public RedisHelper(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task Set<T>(string key, T obj)
    {
        string objStr = JsonConvert.SerializeObject(obj);
        await _redisCache.SetStringAsync(key, objStr);
    }

    public async Task<T> Get<T>(string key)
    {
        string obj = await _redisCache.GetStringAsync(key);
        if (String.IsNullOrEmpty(obj)) return default(T);
        return JsonConvert.DeserializeObject<T>(obj);
    }

    public async Task Clear(string key)
    {
        await _redisCache.RemoveAsync(key);
    }
}
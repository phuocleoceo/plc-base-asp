using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Net;

namespace PlcBase.Shared.Helpers;

public class RedisHelper : IRedisHelper
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDistributedCache _redisCache;
    private readonly CacheSettings _cacheSettings;
    private readonly IDatabase _redisDatabase;

    public RedisHelper(
        IDistributedCache redisCache,
        IOptions<CacheSettings> cacheSettings,
        IConnectionMultiplexer connectionMultiplexer
    )
    {
        _redisCache = redisCache;
        _cacheSettings = cacheSettings.Value;
        _connectionMultiplexer = connectionMultiplexer;
        _redisDatabase = connectionMultiplexer.GetDatabase();
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

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
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

    public async Task ClearByPattern(string pattern)
    {
        await foreach (string key in GetKeysMatchPattern(pattern))
        {
            await _redisCache.RemoveAsync(key);
        }
    }

    private async IAsyncEnumerable<string> GetKeysMatchPattern(string pattern)
    {
        foreach (EndPoint endpoint in _connectionMultiplexer.GetEndPoints())
        {
            IServer server = _connectionMultiplexer.GetServer(endpoint);
            await foreach (RedisKey key in server.KeysAsync(pattern: pattern))
            {
                yield return key.ToString();
            }
        }
    }

    public async Task<T> GetCachedOr<T>(string key, Func<T> supplier)
    {
        if (!_cacheSettings.Enable)
        {
            return supplier.Invoke();
        }

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

    public async Task<T> GetCachedOr<T>(string key, Func<Task<T>> supplier)
    {
        if (!_cacheSettings.Enable)
        {
            return await supplier.Invoke();
        }

        T cachedData = await Get<T>(key);
        if (cachedData != null)
        {
            return cachedData;
        }

        T data = await supplier.Invoke();
        if (data == null)
        {
            return default;
        }

        await SetWithTtl(key, data);
        return data;
    }

    public Task SetMapCache<T>(string mapKey, string itemKey, T itemValue, bool clearCurrentMap)
    {
        throw new NotImplementedException();
    }

    public Task SetMapCache<T>(string mapKey, Dictionary<string, T> items, bool clearCurrentMap)
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<string, T>> GetMapCache<T>(string mapKey, HashSet<string> itemKeys)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetMapCache<T>(string mapKey, string itemKey)
    {
        throw new NotImplementedException();
    }

    public Task ClearMapCache(string mapKey)
    {
        throw new NotImplementedException();
    }

    public Task RemoveMapCache(string mapKey, string itemKey)
    {
        throw new NotImplementedException();
    }

    public Task RemoveMapCache(string mapKey, HashSet<string> itemKeys)
    {
        throw new NotImplementedException();
    }
}

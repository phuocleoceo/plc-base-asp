using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Net;

using PlcBase.Shared.Utilities;

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
        string objStr = JsonUtility.Stringify(obj);
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

        string objStr = JsonUtility.Stringify(obj);
        await _redisCache.SetStringAsync(key, objStr, options);
    }

    public async Task<T> Get<T>(string key)
    {
        string obj = await _redisCache.GetStringAsync(key);
        return JsonUtility.Parse<T>(obj);
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

    public async Task SetMapCache<T>(
        string mapKey,
        string itemKey,
        T itemValue,
        bool clearCurrentMap
    )
    {
        if (clearCurrentMap)
        {
            await _redisDatabase.KeyDeleteAsync(mapKey);
        }

        await _redisDatabase.HashSetAsync(mapKey, itemKey, JsonUtility.Stringify(itemValue));
    }

    public async Task SetMapCache<T>(
        string mapKey,
        Dictionary<string, T> items,
        bool clearCurrentMap
    )
    {
        if (clearCurrentMap)
        {
            await _redisDatabase.KeyDeleteAsync(mapKey);
        }

        HashEntry[] entries = items
            .Select(item => new HashEntry(item.Key, JsonUtility.Stringify(item.Value)))
            .ToArray();

        await _redisDatabase.HashSetAsync(mapKey, entries);
    }

    public async Task<Dictionary<string, T>> GetMapCache<T>(string mapKey, HashSet<string> itemKeys)
    {
        var result = new Dictionary<string, T>();
        foreach (var itemKey in itemKeys)
        {
            var value = await _redisDatabase.HashGetAsync(mapKey, itemKey);
            if (!value.IsNull)
            {
                result[itemKey] = JsonUtility.Parse<T>(value);
            }
        }
        return result;
    }

    public async Task<T> GetMapCache<T>(string mapKey, string itemKey)
    {
        var value = await _redisDatabase.HashGetAsync(mapKey, itemKey);
        return JsonUtility.Parse<T>(value);
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

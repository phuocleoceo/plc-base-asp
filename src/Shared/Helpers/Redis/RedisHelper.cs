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

    public async Task<List<T>> GetListCache<T>(string key)
    {
        RedisValue[] redisValues = await _redisDatabase.ListRangeAsync(key);

        return redisValues.Length > 0
            ? redisValues.Select(redisValue => JsonUtility.Parse<T>(redisValue)).ToList()
            : null;
    }

    public async Task SetListCache<T>(
        string key,
        List<T> items,
        bool hasExpireTime = true,
        bool clearCurrentList = false
    )
    {
        RedisValue[] redisValues = items
            .ConvertAll(item => (RedisValue)JsonUtility.Stringify(item))
            .ToArray();

        if (clearCurrentList)
        {
            await ClearListCache(key);
        }

        await _redisDatabase.ListRightPushAsync(key, redisValues);

        if (hasExpireTime)
        {
            await _redisDatabase.KeyExpireAsync(key, TimeSpan.FromSeconds(_cacheSettings.Expires));
        }
    }

    public async Task<List<T>> GetElementAtListCache<T>(string key, long index)
    {
        RedisValue redisValue = await _redisDatabase.ListGetByIndexAsync(key, index);
        return !redisValue.IsNull ? JsonUtility.Parse<List<T>>(redisValue) : null;
    }

    public async Task SetElementAtListCache<T>(
        string key,
        long index,
        T item,
        bool hasExpireTime = true
    )
    {
        await _redisDatabase.ListSetByIndexAsync(key, index, JsonUtility.Stringify(item));

        if (hasExpireTime)
        {
            await _redisDatabase.KeyExpireAsync(key, TimeSpan.FromSeconds(_cacheSettings.Expires));
        }
    }

    public async Task ClearListCache(string key)
    {
        await _redisDatabase.KeyDeleteAsync(key);
    }

    public async Task RemoveElementAtListCache(string key, long index)
    {
        await _redisDatabase.ListRemoveAsync(key, index);
    }

    public async Task SetMapCache<T>(
        string mapKey,
        string itemKey,
        T itemValue,
        bool hasExpireTime = true,
        bool clearCurrentMap = false
    )
    {
        if (clearCurrentMap)
        {
            await ClearMapCache(mapKey);
        }

        await _redisDatabase.HashSetAsync(mapKey, itemKey, JsonUtility.Stringify(itemValue));

        if (hasExpireTime)
        {
            await _redisDatabase.KeyExpireAsync(
                mapKey,
                TimeSpan.FromSeconds(_cacheSettings.Expires)
            );
        }
    }

    public async Task SetMapCache<T>(
        string mapKey,
        Dictionary<string, T> items,
        bool hasExpireTime = true,
        bool clearCurrentMap = false
    )
    {
        if (clearCurrentMap)
        {
            await ClearMapCache(mapKey);
        }

        HashEntry[] entries = items
            .Select(item => new HashEntry(item.Key, JsonUtility.Stringify(item.Value)))
            .ToArray();

        await _redisDatabase.HashSetAsync(mapKey, entries);

        if (hasExpireTime)
        {
            await _redisDatabase.KeyExpireAsync(
                mapKey,
                TimeSpan.FromSeconds(_cacheSettings.Expires)
            );
        }
    }

    public async Task<Dictionary<string, T>> GetMapCache<T>(string mapKey, HashSet<string> itemKeys)
    {
        RedisValue[] redisKeys = itemKeys.Select(itemKey => (RedisValue)itemKey).ToArray();
        RedisValue[] hashEntries = await _redisDatabase.HashGetAsync(mapKey, redisKeys);

        Dictionary<string, T> result = new Dictionary<string, T>();

        for (int i = 0; i < hashEntries.Length; i++)
        {
            RedisValue value = hashEntries[i];
            if (value.IsNull)
            {
                continue;
            }

            result.Add(itemKeys.ElementAt(i), JsonUtility.Parse<T>(value));
        }

        return result;
    }

    public async Task<T> GetMapCache<T>(string mapKey, string itemKey)
    {
        RedisValue value = await _redisDatabase.HashGetAsync(mapKey, itemKey);
        return !value.IsNull ? JsonUtility.Parse<T>(value) : default;
    }

    public async Task ClearMapCache(string mapKey)
    {
        await _redisDatabase.KeyDeleteAsync(mapKey);
    }

    public async Task RemoveMapCache(string mapKey, string itemKey)
    {
        await _redisDatabase.HashDeleteAsync(mapKey, itemKey);
    }

    public async Task RemoveMapCache(string mapKey, IEnumerable<string> itemKeys)
    {
        RedisValue[] redisKeys = itemKeys.Select(itemKey => (RedisValue)itemKey).ToArray();
        await _redisDatabase.HashDeleteAsync(mapKey, redisKeys);
    }
}

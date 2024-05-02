namespace PlcBase.Shared.Helpers;

public interface IRedisHelper
{
    Task Set<T>(string key, T obj);

    Task SetWithTtl<T>(string key, T obj);

    Task<T> Get<T>(string key);

    Task Clear(string key);

    Task ClearByPattern(string pattern);

    Task<T> GetCachedOr<T>(string key, Func<T> supplier);

    Task<T> GetCachedOr<T>(string key, Func<Task<T>> supplier);

    Task SetMapCache<T>(
        string mapKey,
        string itemKey,
        T itemValue,
        bool hasExpireTime = true,
        bool clearCurrentMap = false
    );

    Task SetMapCache<T>(
        string mapKey,
        Dictionary<string, T> items,
        bool hasExpireTime = true,
        bool clearCurrentMap = false
    );

    Task<Dictionary<string, T>> GetMapCache<T>(string mapKey, HashSet<string> itemKeys);

    Task<T> GetMapCache<T>(string mapKey, string itemKey);

    Task ClearMapCache(string mapKey);

    Task RemoveMapCache(string mapKey, string itemKey);

    Task RemoveMapCache(string mapKey, IEnumerable<string> itemKeys);
}

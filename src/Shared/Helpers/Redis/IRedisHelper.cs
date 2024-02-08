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
}

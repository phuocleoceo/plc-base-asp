namespace PlcBase.Shared.Helpers;

public interface IRedisHelper
{
    Task Set<T>(string key, T obj);

    Task SetWithTtl<T>(string key, T obj);

    Task<T> Get<T>(string key);

    Task Clear(string key);

    Task<T> GetCachedOr<T>(string key, Func<T> supplier);

    Task<List<T>> GetListCachedOr<T>(string key, Func<List<T>> supplier);
}

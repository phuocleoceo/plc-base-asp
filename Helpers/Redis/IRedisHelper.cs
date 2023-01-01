namespace Monolithic.Helpers;

public interface IRedisHelper
{
    Task Set<T>(string key, T obj);

    Task<T> Get<T>(string key);

    Task Clear(string key);
}
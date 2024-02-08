using BC = BCrypt.Net.BCrypt;

namespace PlcBase.Shared.Utilities;

public static class RedisUtility
{
    public static string GetKey<T>()
    {
        return typeof(T).ToString();
    }
}

namespace PlcBase.Shared.Utilities;

public static class RedisUtility
{
    private const string KEY_PATTERN = "plc-base:{0}:{1}";

    public static string GetKey<T>(int? id = null)
    {
        return string.Format(KEY_PATTERN, typeof(T), id?.ToString() ?? "");
    }

    public static string GetListKey<T>()
    {
        return string.Format(KEY_PATTERN, typeof(T), "list");
    }
}

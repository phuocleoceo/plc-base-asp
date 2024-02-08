namespace PlcBase.Shared.Utilities;

public static class RedisUtility
{
    private const string KEY_PATTERN = "plc-base:{0}:{1}";

    public static string GetKey<T>(string postfix = "")
    {
        return string.Format(KEY_PATTERN, typeof(T).Name.ToLower(), postfix);
    }

    public static string GetKeyById<T>(int? id = null)
    {
        return GetKey<T>(id?.ToString() ?? "");
    }

    public static string GetListKey<T>()
    {
        return GetKey<T>("list");
    }
}

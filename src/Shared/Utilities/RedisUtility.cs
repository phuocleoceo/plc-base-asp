namespace PlcBase.Shared.Utilities;

public static class RedisUtility
{
    private const string SERVICE_NAME = "plc-base";
    private const string CACHE_KEY_PATTERN = SERVICE_NAME + ":{0}:{1}";
    private const string CLEAR_KEY_PATTERN = SERVICE_NAME + ":*{0}*";

    public static string GetKey<T>(string postfix = "")
    {
        return string.Format(CACHE_KEY_PATTERN, GetClassName<T>(), postfix);
    }

    public static string GetClearKey<T>()
    {
        return string.Format(CLEAR_KEY_PATTERN, GetClassName<T>());
    }

    public static string GetKeyById<T>(int? id = null)
    {
        return GetKey<T>(id?.ToString() ?? "");
    }

    public static string GetListKey<T>()
    {
        return GetKey<T>("list");
    }

    private static string GetClassName<T>()
    {
        return typeof(T).Name.ToLower();
    }
}

using Newtonsoft.Json;

namespace PlcBase.Shared.Utilities;

public static class JsonUtility
{
    public static string Stringify<T>(
        T obj,
        Formatting formatting = Formatting.None,
        JsonSerializerSettings settings = null
    )
    {
        if (obj == null)
        {
            return "";
        }

        try
        {
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static T Parse<T>(string objString)
    {
        if (string.IsNullOrWhiteSpace(objString))
        {
            return default;
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(objString);
        }
        catch (Exception)
        {
            return default;
        }
    }
}

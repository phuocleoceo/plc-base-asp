using Newtonsoft.Json;
using PlcBase.Shared.Utilities;

namespace PlcBase.Shared.Helpers;

public class LogContent
{
    public string Path { get; set; }

    public string Method { get; set; }

    public string Params { get; set; }

    public string Message { get; set; }

    public int StatusCode { get; set; }

    public override string ToString()
    {
        return JsonUtility.Stringify(this);
    }
}

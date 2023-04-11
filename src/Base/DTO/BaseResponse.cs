using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using PlcBase.Shared.Constants;

namespace PlcBase.Base.DTO;

public class BaseResponse<T>
{
    public T Data { get; set; } = default(T);

    public int StatusCode { get; set; } = HttpCode.OK;

    public string Message { get; set; } = null;

    public Dictionary<string, string[]> Errors { get; set; } = null;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(
            this,
            Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
        );
    }
}

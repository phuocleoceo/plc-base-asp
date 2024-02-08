using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using PlcBase.Shared.Constants;

namespace PlcBase.Base.DTO;

public class SuccessResponse<T> : BaseResponse
{
    public T Data { get; set; }

    public SuccessResponse()
    {
        Data = default;
    }

    public SuccessResponse(T data, int statusCode = HttpCode.OK)
        : base(true, statusCode)
    {
        Data = data;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(
            this,
            Formatting.Indented,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );
    }
}

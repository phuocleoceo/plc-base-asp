using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using PlcBase.Shared.Constants;

namespace PlcBase.Base.DTO;

public class BaseResponse<T>
{
    public T Data { get; set; }

    public int StatusCode { get; set; }

    public string Message { get; set; }

    public BaseResponse() { }
    public BaseResponse(T Data = default(T),
                        int StatusCode = HttpCode.OK,
                        string Message = "")
    {
        this.Data = Data;
        this.StatusCode = StatusCode;
        this.Message = Message;
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
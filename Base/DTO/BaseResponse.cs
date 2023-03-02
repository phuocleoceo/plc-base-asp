using Newtonsoft.Json.Serialization;
using PlcBase.Common.Constants;
using Newtonsoft.Json;

namespace PlcBase.Base.DTO;

public class BaseResponse<T>
{
    public T Data { get; set; }

    public int StatusCode { get; set; }

    public string Message { get; set; }

    public BaseResponse() { }
    public BaseResponse(T Data = default(T),
                        int StatusCode = HttpCode.OK,
                        string Message = null)
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
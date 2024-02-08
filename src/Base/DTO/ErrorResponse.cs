using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using PlcBase.Shared.Constants;

namespace PlcBase.Base.DTO;

public class ErrorResponse : BaseResponse
{
    public string Message { get; set; }

    public Dictionary<string, string[]> Errors { get; set; }

    public ErrorResponse() { }

    public ErrorResponse(
        string message = "",
        int statusCode = HttpCode.BAD_REQUEST,
        Dictionary<string, string[]> errors = null
    )
        : base(false, statusCode)
    {
        Message = message;
        Errors = errors;
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

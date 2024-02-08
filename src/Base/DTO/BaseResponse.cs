using PlcBase.Shared.Constants;

namespace PlcBase.Base.DTO;

public class BaseResponse
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    protected BaseResponse() { }

    protected BaseResponse(bool success = true, int statusCode = HttpCode.OK)
    {
        Success = success;
        StatusCode = statusCode;
    }
}

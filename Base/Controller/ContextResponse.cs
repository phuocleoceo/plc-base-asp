using PlcBase.Shared.Constants;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Controller;

public static class ContextResponse
{
    public static BaseResponse<T> Success<T>(
            this HttpContext context,
            T Data = default(T),
            int StatusCode = HttpCode.OK,
            string Message = null)
    {
        context.Response.StatusCode = StatusCode;
        context.Items["responseMessage"] = Message;

        return new BaseResponse<T>()
        {
            Data = Data,
            StatusCode = StatusCode,
            Message = Message,
        };
    }

    public static dynamic Failure(
            this HttpContext context,
            int StatusCode = HttpCode.INTERNAL_SERVER_ERROR,
            string Message = "")
    {
        throw new BaseException(StatusCode, Message);
    }
}
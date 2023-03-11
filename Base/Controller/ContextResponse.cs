using PlcBase.Common.Constants;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Controller;

public static class ContextResponse
{
    public static BaseResponse<T> Success<T>(
            this HttpContext context,
            T Data = default(T),
            int StatusCode = HttpCode.OK,
            string Message = "")
    {
        context.Response.StatusCode = StatusCode;
        context.Items["responseMessage"] = Message;

        return new BaseResponse<T>(Data, StatusCode, Message);
    }

    public static dynamic Failure(
            this HttpContext context,
            int StatusCode = HttpCode.INTERNAL_SERVER_ERROR,
            string Message = "")
    {
        throw new BaseException(StatusCode, Message);
    }
}
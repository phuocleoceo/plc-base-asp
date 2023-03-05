using PlcBase.Common.Constants;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Controller;

public static class ContextExtension
{
    public static BaseResponse<T> Success<T>(
            this HttpContext context,
            BaseResponse<T> responseData)
    {
        context.Response.StatusCode = responseData.StatusCode;

        context.Items["responseStatusCode"] = responseData.StatusCode;
        context.Items["responseMessage"] = responseData.Message;

        return responseData;
    }

    public static dynamic Failure(
            this HttpContext context,
            int StatusCode = HttpCode.INTERNAL_SERVER_ERROR,
            string Message = "")
    {
        throw new BaseException(StatusCode, Message);
    }
}
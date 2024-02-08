using PlcBase.Shared.Constants;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Controller;

public static class ContextResponse
{
    public static SuccessResponse<T> Success<T>(
        this HttpContext context,
        T data = default,
        int statusCode = HttpCode.OK
    )
    {
        context.Response.StatusCode = statusCode;
        context.Items["responseMessage"] = "";
        return new SuccessResponse<T>(data, statusCode);
    }

    public static dynamic Failure(
        this HttpContext context,
        int statusCode = HttpCode.INTERNAL_SERVER_ERROR,
        string message = ""
    )
    {
        throw new BaseException(statusCode, message);
    }
}

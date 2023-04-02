using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Middlewares;

public static class CustomAuthResponse
{
    public static void UseCustomAuthResponse(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            await next();

            if (context.Response.StatusCode == HttpCode.UNAUTHORIZED)
                throw new BaseException(HttpCode.UNAUTHORIZED, ErrorMessage.UNAUTHORIZED_USER);

            if (context.Response.StatusCode == HttpCode.FORBIDDEN)
                throw new BaseException(HttpCode.FORBIDDEN, ErrorMessage.FORBIDDEN_RESOURCE);
        });
    }
}
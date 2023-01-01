using PlcBase.Common.Constants;
using PlcBase.Models.Common;

namespace PlcBase.Middlewares;

public static class CustomAuthResponse
{
    public static void UseCustomAuthResponse(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == HttpCode.UNAUTHORIZED)
                throw new BaseException(HttpCode.UNAUTHORIZED, "");
            if (context.Response.StatusCode == HttpCode.FORBIDDEN)
                throw new BaseException(HttpCode.FORBIDDEN, "");
        });
    }
}
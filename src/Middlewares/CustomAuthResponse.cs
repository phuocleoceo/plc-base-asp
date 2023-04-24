using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Middlewares;

public static class CustomAuthResponse
{
    public static void UseCustomAuthResponse(this IApplicationBuilder app)
    {
        app.Use(
            async (context, next) =>
            {
                await next();

                switch (context.Response.StatusCode)
                {
                    case HttpCode.UNAUTHORIZED:
                        throw new BaseException(
                            HttpCode.UNAUTHORIZED,
                            ErrorMessage.UNAUTHORIZED_USER
                        );

                    case HttpCode.FORBIDDEN:
                        throw new BaseException(
                            HttpCode.FORBIDDEN,
                            ErrorMessage.FORBIDDEN_RESOURCE
                        );
                }
            }
        );
    }
}

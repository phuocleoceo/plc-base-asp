using Microsoft.AspNetCore.Diagnostics;

using PlcBase.Extensions.Builders;
using PlcBase.Shared.Constants;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Middlewares;

public static class ErrorHandler
{
    public static void ConfigureErrorHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                IExceptionHandlerFeature exceptionHandlerFeature =
                    context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature == null)
                    return;

                Exception exception = exceptionHandlerFeature.Error;

                context.Response.StatusCode = HttpCode.INTERNAL_SERVER_ERROR;
                context.Response.ContentType = "application/json";

                string message = exception.Message;

                int statusCode =
                    exception.GetType() == typeof(BaseException)
                        ? ((BaseException)exception).StatusCode
                        : HttpCode.INTERNAL_SERVER_ERROR;

                Dictionary<string, string[]> errors =
                    exception.GetType() == typeof(BaseException)
                        ? ((BaseException)exception).Errors
                        : null;

                logger.LogErrorResponse(context, message, statusCode);

                await context.WriteErrorResponse(message, statusCode, errors);
            });
        });
    }

    private static void LogErrorResponse(
        this ILoggerManager logger,
        HttpContext context,
        string message,
        int statusCode
    )
    {
        string logContent = context.GetLogContent(message, statusCode);
        logger.LogError(logContent);
    }

    private static async Task WriteErrorResponse(
        this HttpContext context,
        string message,
        int statusCode,
        Dictionary<string, string[]> errors
    )
    {
        context.Response.StatusCode = statusCode;
        context.Items["isError"] = true;

        await context.Response.WriteAsync(
            new ErrorResponse(message, statusCode, errors).ToString() ?? string.Empty
        );
    }
}

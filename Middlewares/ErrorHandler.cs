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
                IExceptionHandlerPathFeature exceptionHandlerPathFeature =
                                    context.Features.Get<IExceptionHandlerPathFeature>();
                Exception exception = exceptionHandlerPathFeature.Error;

                context.Response.StatusCode = HttpCode.INTERNAL_SERVER_ERROR;
                context.Response.ContentType = "application/json";

                IExceptionHandlerFeature contextFeature =
                                    context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    string message = exception.Message;
                    int statusCode = exception.GetType() == typeof(BaseException) ?
                                            ((BaseException)exception).StatusCode :
                                            HttpCode.INTERNAL_SERVER_ERROR;

                    logger.LogErrorResponse(context, message, statusCode);

                    await context.WriteErrorResponse(message, statusCode);
                }
            });
        });
    }

    private static void LogErrorResponse(this ILoggerManager logger,
                                         HttpContext context,
                                         string message,
                                         int statusCode)
    {
        string logContent = context.GetLogContent(message, statusCode);
        logger.LogError(logContent);
    }

    private static async Task WriteErrorResponse(this HttpContext context,
                                                 string message,
                                                 int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Items["isError"] = true;

        await context.Response.WriteAsync(new BaseResponse<string>()
        {
            StatusCode = statusCode,
            Message = message
        }.ToString());
    }
}
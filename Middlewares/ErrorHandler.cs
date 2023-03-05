using Microsoft.AspNetCore.Diagnostics;
using PlcBase.Extensions.Builders;
using PlcBase.Common.Constants;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;
using PlcBase.Helpers;

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
                    int statusCode = exception.GetType() == typeof(BaseException) ?
                                            ((BaseException)exception).StatusCode :
                                            HttpCode.INTERNAL_SERVER_ERROR;

                    string logContent = context.GetLogContent(exception.Message, statusCode);
                    logger.LogError(logContent);

                    context.Response.StatusCode = statusCode;
                    context.Items["isError"] = true;

                    await context.Response.WriteAsync(new BaseResponse<string>()
                    {
                        StatusCode = statusCode,
                        Message = exception.Message
                    }.ToString());
                }
            });
        });
    }
}
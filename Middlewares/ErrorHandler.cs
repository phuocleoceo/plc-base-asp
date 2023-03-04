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
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                context.Response.StatusCode = HttpCode.INTERNAL_SERVER_ERROR;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var statusCode = exception.GetType() == typeof(BaseException) ?
                                            ((BaseException)exception).StatusCode :
                                            HttpCode.INTERNAL_SERVER_ERROR;

                    var LogContent = context.GetLogContent(exception.Message, statusCode);
                    logger.LogError(LogContent);

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
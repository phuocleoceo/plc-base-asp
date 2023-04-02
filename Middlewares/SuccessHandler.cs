using PlcBase.Extensions.Builders;
using PlcBase.Shared.Helpers;

namespace PlcBase.Middlewares;

public static class SuccessHandler
{
    public static void ConfigureSuccessHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.Use(async (context, next) =>
        {
            await next();

            logger.LogSuccessResponse(context);
        });
    }

    private static void LogSuccessResponse(this ILoggerManager logger, HttpContext context)
    {
        // If context item is null or false => isError = false
        bool isError = Convert.ToBoolean(context.Items["isError"]);

        if (!isError)
        {
            string responseMessage = Convert.ToString(context.Items["responseMessage"]);
            string logContent = context.GetLogContent(responseMessage, context.Response.StatusCode);
            logger.LogInformation(logContent);
        }
    }
}
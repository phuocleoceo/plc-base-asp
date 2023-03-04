using PlcBase.Extensions.Builders;
using PlcBase.Helpers;

namespace PlcBase.Middlewares;

public static class SuccessHandler
{
    public static void ConfigureSuccessHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.Use(async (context, next) =>
        {
            await next();

            // If context item is null or false => isError = false
            bool isError = Convert.ToBoolean(context.Items["isError"]);

            if (!isError)
            {
                string logContent = context.GetLogContent();
                logger.LogInformation(logContent);
            }
        });
    }
}
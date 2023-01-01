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
            logger.LogInformation(context.GetLogContent());
        });
    }
}
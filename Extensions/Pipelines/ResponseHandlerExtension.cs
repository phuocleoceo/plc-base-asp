using Monolithic.Middlewares;
using Monolithic.Helpers;

namespace Monolithic.Extensions.Pipelines;

public static class ResponseHandlerExtension
{
    public static void UseResponseHandlerPipeline(this WebApplication app, ILoggerManager logger)
    {
        app.ConfigureSuccessHandler(logger);
        app.ConfigureErrorHandler(logger);
        app.UseCustomAuthResponse();
    }
}
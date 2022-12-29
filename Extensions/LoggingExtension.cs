using Monolithic.Constants;
using Serilog.Exceptions;
using Monolithic.Helpers;
using Serilog.Events;
using Serilog;

namespace Monolithic.Extensions;

public static class LoggingExtension
{
    public static void ConfigureSerilog(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        var logSettings = configuration.GetSection("LogSettings").Get<LogSettings>();

        if (logSettings.Enable)
        {
            var log = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("Monolithic", LogEventLevel.Information)
                        .MinimumLevel.Override("Savorboard", LogEventLevel.Error)
                        .MinimumLevel.Override("DotNetCore.CAP", LogEventLevel.Error)
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Console()
                        .CreateLogger();

            loggingBuilder.AddSerilog(log);
        }
    }

    public static string GetLogContent(this HttpContext context,
                                       string message = "",
                                       int statusCode = HttpCode.OK)
    {
        LogContent content = new LogContent
        {
            Path = context.Request.Path,
            Method = context.Request.Method,
            Params = "",
            Message = message,
            StatusCode = statusCode,
        };
        QueryString queryString = context.Request.QueryString;
        if (queryString.HasValue)
        {
            content.Params = queryString.Value;
        }
        return content.ToString();
    }
}
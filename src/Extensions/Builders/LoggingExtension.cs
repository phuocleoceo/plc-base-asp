using Serilog.Core;
using Serilog;

using PlcBase.Shared.Constants;
using PlcBase.Shared.Helpers;

namespace PlcBase.Extensions.Builders;

public static class LoggingExtension
{
    public static void ConfigureSerilog(
        this ILoggingBuilder loggingBuilder,
        IConfiguration configuration
    )
    {
        LogSettings logSettings = configuration.GetSection("LogSettings").Get<LogSettings>();

        if (logSettings.Enable)
        {
            Logger log = new LoggerConfiguration().ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            loggingBuilder.AddSerilog(log);
        }
    }

    public static string GetLogContent(
        this HttpContext context,
        string message = null,
        int statusCode = HttpCode.OK
    )
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

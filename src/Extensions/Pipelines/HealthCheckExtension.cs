using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace PlcBase.Extensions.Pipelines;

public static class HealthCheckExtension
{
    public static void UseHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks(
            "/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );
    }
}

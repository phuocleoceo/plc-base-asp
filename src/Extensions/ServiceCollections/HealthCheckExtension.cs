namespace PlcBase.Extensions.ServiceCollections;

public static class HealthCheckExtension
{
    public static void ConfigureHealthCheck(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHealthChecks();
    }
}

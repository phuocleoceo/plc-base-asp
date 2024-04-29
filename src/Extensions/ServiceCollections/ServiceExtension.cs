namespace PlcBase.Extensions.ServiceCollections;

public static class ServiceExtension
{
    public static void ConfigureService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // CORS
        services.ConfigureCors(configuration);
        // CAP message queue
        services.ConfigureCapQueue();
        // Database
        services.ConfigureDataContext(configuration);
        // Cache
        services.ConfigureRedis(configuration);
        // Internal Data Factory
        services.ConfigureDataFactory(configuration);
        // Authentication
        services.ConfigureAuth(configuration);
        // Add Http client
        services.AddHttpClient();
        // Controller mapper
        services.AddControllers();
        // Generate lowercase URLs
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        // Swagger
        services.ConfigureSwagger(configuration);
        // AWS
        services.ConfigureAWS(configuration);
    }
}

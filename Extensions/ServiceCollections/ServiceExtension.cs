namespace Monolithic.Extensions.ServiceCollections;

public static class ServiceExtension
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        // CORS
        services.ConfigureCors(configuration);
        // Database
        services.ConfigureDataContext(configuration);
        // Appsetting model
        services.ConfigureAppSetting(configuration);
        // Dependency Injection
        services.ConfigureDI();
        // Authentication
        services.ConfigureAuth(configuration);
        // CAP message queue
        services.ConfigureCapQueue();
        // Add Http client
        services.AddHttpClient();
        // Controller mapper
        services.AddControllers();
        // // generate lowercase URLs
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        // Swagger
        services.ConfigureSwagger(configuration);
    }
}
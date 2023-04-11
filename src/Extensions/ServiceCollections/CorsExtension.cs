namespace PlcBase.Extensions.ServiceCollections;

public static class CorsExtension
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(
            c =>
                c.AddDefaultPolicy(options =>
                {
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                })
        );
    }
}

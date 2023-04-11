using Microsoft.OpenApi.Models;

namespace PlcBase.Extensions.ServiceCollections;

public static class SwaggerExtension
{
    public static void ConfigureSwagger(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddEndpointsApiExplorer();
        services.ConfigureSwaggerDoc(configuration);
        services.ConfigureSwaggerAuth();
    }

    private static void ConfigureSwaggerDoc(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PLC API", Version = "v1", });
        });
    }

    private static void ConfigureSwaggerAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        @"Enter 'Bearer ' + your Token   
                                    Example: Bearer 123456789",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                }
            );
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                }
            );
        });
    }
}

using Microsoft.EntityFrameworkCore;
using Monolithic.Models.Context;
using Monolithic.Models.Mapper;
using Microsoft.OpenApi.Models;
using Monolithic.Helpers;

namespace Monolithic.Extensions;

public static class ServiceExtension
{
    public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        string currentDatabaseConfig = configuration.GetSection("CurrentDatabaseConfig").Value;
        string cns = configuration.GetConnectionString(currentDatabaseConfig);
        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(cns, ServerVersion.AutoDetect(cns));
        });
    }

    public static void ConfigureModelSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }

    public static void ConfigureDI(this IServiceCollection services)
    {
        services.ConfigureLibraryDI();
        services.ConfigureRepositoryDI();
        services.ConfigureServiceDI();
        services.ConfigCommonServiceDI();
        services.ConfigureHelperDI();
        services.ConfigSwagger();
    }

    private static void ConfigureLibraryDI(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
    }

    private static void ConfigureRepositoryDI(this IServiceCollection services)
    {
        
    }

    private static void ConfigureServiceDI(this IServiceCollection services)
    {
        
    }

    private static void ConfigCommonServiceDI(this IServiceCollection services)
    {

    }

    private static void ConfigureHelperDI(this IServiceCollection services)
    {
        services.AddScoped<ISendMailHelper, SendMailHelper>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    private static void ConfigSwagger(this IServiceCollection services)
    {
        // Register the Swagger generator, defining 1 or more Swagger documents
        services.AddSwaggerGen(c =>
        {
            string version = "v1";
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API PBL6", Version = version });
        });
    }
}
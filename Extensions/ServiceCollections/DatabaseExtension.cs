using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Context;

namespace PlcBase.Extensions.ServiceCollections;

public static class DatabaseExtension
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
}
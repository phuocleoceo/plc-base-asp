using Microsoft.EntityFrameworkCore;

using PlcBase.Common.Data.Context;

namespace PlcBase.Extensions.ServiceCollections;

public static class DatabaseExtension
{
    public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        string selectedDatabase = configuration.GetSection("SelectedDatabase").Value;
        string connectionString = configuration.GetConnectionString(selectedDatabase);

        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }
}

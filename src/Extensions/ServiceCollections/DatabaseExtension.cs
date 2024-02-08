using Microsoft.EntityFrameworkCore;

using PlcBase.Common.Data.Context;

namespace PlcBase.Extensions.ServiceCollections;

public static class DatabaseExtension
{
    public static void ConfigureDataContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string selectedDatabase = configuration.GetSection("SelectedDatabase").Value;
        if (selectedDatabase == null)
            return;

        string connectionString = configuration.GetConnectionString(selectedDatabase);
        if (connectionString == null)
            return;

        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }
}

using Microsoft.EntityFrameworkCore;

using PlcBase.Common.Data.Context;

namespace PlcBase.Extensions.Pipelines;

public static class MigrationExtension
{
    public static void UseMigration(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        DataContext context = services.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }
}

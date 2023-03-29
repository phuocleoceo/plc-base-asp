using PlcBase.Repositories.Implement;
using PlcBase.Repositories.Interface;

namespace PlcBase.Repositories;

public static class RepositoryDIExtension
{
    public static void ConfigureRepositoryDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
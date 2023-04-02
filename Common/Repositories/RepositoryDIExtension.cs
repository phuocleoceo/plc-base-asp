namespace PlcBase.Common.Repositories;

public static class RepositoryDIExtension
{
    public static void ConfigureRepositoryDI(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
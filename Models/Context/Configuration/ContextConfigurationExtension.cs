using Microsoft.EntityFrameworkCore;

namespace PlcBase.Models.Context.Configuration;

public static class ContextConfigurationExtension
{
    public static void ApplyEntityConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermisisonConfiguration());
    }
}
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Common.Data.Context.Configuration;

public static class ContextConfigurationExtension
{
    public static void ApplyEntityConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AddressProvinceConfiguration());
        modelBuilder.ApplyConfiguration(new AddressDistrictConfiguration());
        modelBuilder.ApplyConfiguration(new AddressWardConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermisisonConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigSettingConfiguration());
        modelBuilder.ApplyConfiguration(new MediaConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectMemberConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectStatusConfiguration());
    }
}

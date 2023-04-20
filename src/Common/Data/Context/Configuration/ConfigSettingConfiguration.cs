using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.ConfigSetting.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class ConfigSettingConfiguration : IEntityTypeConfiguration<ConfigSettingEntity>
{
    public void Configure(EntityTypeBuilder<ConfigSettingEntity> builder)
    {
        builder.HasIndex(c => new { c.Key, }).IsUnique();
    }
}

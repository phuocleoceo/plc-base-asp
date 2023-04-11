using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.AccessControl.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasIndex(c => new { c.Name, }).IsUnique();
    }
}

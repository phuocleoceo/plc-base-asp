using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.AccessControl.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class PermisisonConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasIndex(c => new { c.Key, }).IsUnique();

        builder.HasIndex(c => new { c.RoleId, });
    }
}

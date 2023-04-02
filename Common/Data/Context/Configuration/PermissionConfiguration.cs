using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.AccessControl.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

public class PermisisonConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.Key,
        }).IsUnique();

        builder.HasIndex(c => new
        {
            c.RoleId,
        });
    }
}
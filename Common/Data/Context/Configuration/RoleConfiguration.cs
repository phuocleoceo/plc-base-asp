using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.AccessControl.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.Name,
        }).IsUnique();
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class ProjectPermissionConfiguration : IEntityTypeConfiguration<ProjectPermissionEntity>
{
    public void Configure(EntityTypeBuilder<ProjectPermissionEntity> builder)
    {
        builder.HasIndex(c => new { c.RoleId });
    }
}

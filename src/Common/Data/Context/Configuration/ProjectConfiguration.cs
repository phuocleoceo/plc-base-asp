using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Project.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.HasIndex(c => new { c.CreatorId, }).IsUnique();
    }
}

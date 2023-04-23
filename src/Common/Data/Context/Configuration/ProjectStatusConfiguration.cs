using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.ProjectStatus.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class ProjectStatusConfiguration : IEntityTypeConfiguration<ProjectStatusEntity>
{
    public void Configure(EntityTypeBuilder<ProjectStatusEntity> builder)
    {
        builder.HasIndex(c => new { c.ProjectId });
    }
}

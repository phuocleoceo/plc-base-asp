using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Sprint.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class SprintConfiguration : IEntityTypeConfiguration<SprintEntity>
{
    public void Configure(EntityTypeBuilder<SprintEntity> builder)
    {
        builder.HasIndex(c => new { c.ProjectId });
    }
}

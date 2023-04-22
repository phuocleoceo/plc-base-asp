using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Media.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class MediaConfiguration : IEntityTypeConfiguration<MediaEntity>
{
    public void Configure(EntityTypeBuilder<MediaEntity> builder)
    {
        builder.HasIndex(c => new { c.EntityId, c.EntityType });
    }
}

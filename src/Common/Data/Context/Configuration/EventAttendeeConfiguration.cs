using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Event.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class EventAttendeeConfiguration : IEntityTypeConfiguration<EventAttendeeEntity>
{
    public void Configure(EntityTypeBuilder<EventAttendeeEntity> builder)
    {
        builder.HasIndex(c => new { c.UserId, });

        builder.HasIndex(c => new { c.EventId, });
    }
}

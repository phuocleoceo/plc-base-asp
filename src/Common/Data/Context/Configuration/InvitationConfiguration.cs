using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Invitation.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class InvitationConfiguration : IEntityTypeConfiguration<InvitationEntity>
{
    public void Configure(EntityTypeBuilder<InvitationEntity> builder)
    {
        builder.HasIndex(c => new { c.RecipientId, });

        builder.HasIndex(c => new { c.ProjectId, });
    }
}

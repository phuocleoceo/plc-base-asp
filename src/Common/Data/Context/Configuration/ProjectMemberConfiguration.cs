using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.ProjectMember.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMemberEntity>
{
    public void Configure(EntityTypeBuilder<ProjectMemberEntity> builder)
    {
        builder.HasIndex(c => new { c.UserId, c.ProjectId }).IsUnique();
    }
}

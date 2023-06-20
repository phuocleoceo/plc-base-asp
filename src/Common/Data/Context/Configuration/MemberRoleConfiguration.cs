using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class MemberRoleConfiguration : IEntityTypeConfiguration<MemberRoleEntity>
{
    public void Configure(EntityTypeBuilder<MemberRoleEntity> builder)
    {
        builder.HasIndex(c => new { c.ProjectMemberId, });
    }
}

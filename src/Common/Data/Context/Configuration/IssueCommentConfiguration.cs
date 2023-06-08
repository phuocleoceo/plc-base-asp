using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Issue.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class IssueCommentConfiguration : IEntityTypeConfiguration<IssueCommentEntity>
{
    public void Configure(EntityTypeBuilder<IssueCommentEntity> builder)
    {
        builder.HasIndex(c => new { c.IssueId });
    }
}

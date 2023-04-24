using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Issue.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class IssueConfiguration : IEntityTypeConfiguration<IssueEntity>
{
    public void Configure(EntityTypeBuilder<IssueEntity> builder)
    {
        builder.HasIndex(
            c =>
                new
                {
                    c.ProjectId,
                    c.SprintId,
                    c.AssigneeId
                }
        );
    }
}

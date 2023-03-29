using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Entities;

namespace PlcBase.Models.Context.Configuration;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfileEntity>
{
    public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.UserAccountId,
        }).IsUnique();
    }
}
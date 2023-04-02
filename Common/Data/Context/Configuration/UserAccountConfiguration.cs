using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.User.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccountEntity>
{
    public void Configure(EntityTypeBuilder<UserAccountEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.Email,
        }).IsUnique();
    }
}
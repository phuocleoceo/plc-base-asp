using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

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
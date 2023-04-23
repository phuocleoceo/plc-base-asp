using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.User.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfileEntity>
{
    public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
    {
        builder
            .HasIndex(
                c =>
                    new
                    {
                        c.UserAccountId,
                        c.IdentityNumber,
                        c.PhoneNumber
                    }
            )
            .IsUnique();
    }
}

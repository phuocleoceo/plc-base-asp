using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Entities;

namespace PlcBase.Models.Context.Configuration;

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
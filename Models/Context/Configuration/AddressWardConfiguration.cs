using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Entities;

namespace PlcBase.Models.Context.Configuration;

public class AddressWardConfiguration : IEntityTypeConfiguration<AddressWardEntity>
{
    public void Configure(EntityTypeBuilder<AddressWardEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.AddressDistrictId,
        });
    }
}
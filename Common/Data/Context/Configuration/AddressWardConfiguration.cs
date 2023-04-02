using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.Address.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

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
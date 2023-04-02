using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.Address.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

public class AddressDistrictConfiguration : IEntityTypeConfiguration<AddressDistrictEntity>
{
    public void Configure(EntityTypeBuilder<AddressDistrictEntity> builder)
    {
        builder.HasIndex(c => new
        {
            c.AddressProvinceId,
        });
    }
}
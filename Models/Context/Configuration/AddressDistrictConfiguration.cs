using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Entities;

namespace PlcBase.Models.Context.Configuration;

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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Address.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class AddressDistrictConfiguration : IEntityTypeConfiguration<AddressDistrictEntity>
{
    public void Configure(EntityTypeBuilder<AddressDistrictEntity> builder)
    {
        builder.HasIndex(c => new { c.AddressProvinceId, });
    }
}

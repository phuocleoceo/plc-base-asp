using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Address.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class AddressWardConfiguration : IEntityTypeConfiguration<AddressWardEntity>
{
    public void Configure(EntityTypeBuilder<AddressWardEntity> builder)
    {
        builder.HasIndex(c => new { c.AddressDistrictId, });
    }
}

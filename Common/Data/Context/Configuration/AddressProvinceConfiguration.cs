using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Address.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class AddressProvinceConfiguration : IEntityTypeConfiguration<AddressProvinceEntity>
{
    public void Configure(EntityTypeBuilder<AddressProvinceEntity> builder)
    {

    }
}
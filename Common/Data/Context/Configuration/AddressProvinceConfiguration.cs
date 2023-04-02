using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcBase.Features.Address.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlcBase.Shared.Data.Context.Configuration;

public class AddressProvinceConfiguration : IEntityTypeConfiguration<AddressProvinceEntity>
{
    public void Configure(EntityTypeBuilder<AddressProvinceEntity> builder)
    {

    }
}
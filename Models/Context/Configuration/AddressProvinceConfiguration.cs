using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlcBase.Models.Entities;

namespace PlcBase.Models.Context.Configuration;

public class AddressProvinceConfiguration : IEntityTypeConfiguration<AddressProvinceEntity>
{
    public void Configure(EntityTypeBuilder<AddressProvinceEntity> builder)
    {

    }
}
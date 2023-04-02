using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Address.Entities;

[Table(TableName.ADDRESS_PROVINCE)]
public class AddressProvinceEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    public ICollection<AddressDistrictEntity> AddressDistricts { get; set; }
}
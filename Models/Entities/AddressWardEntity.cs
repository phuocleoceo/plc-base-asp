using System.ComponentModel.DataAnnotations.Schema;
using PlcBase.Common.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Models.Entities;

[Table(TableName.ADDRESS_WARD)]
public class AddressWardEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [ForeignKey(nameof(AddressDistrict))]
    [Column("district_id")]
    public int AddressDistrictId { get; set; }

    public AddressDistrictEntity AddressDistrict { get; set; }
}
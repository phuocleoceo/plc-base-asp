using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlcBase.Common.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Models.Entities;

[Table(TableName.ADDRESS_WARD)]
public class AddressWardEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [ForeignKey(nameof(AddressDistrict))]
    [Column("district_id")]
    public int AddressDistrictId { get; set; }

    public AddressDistrictEntity AddressDistrict { get; set; }
}
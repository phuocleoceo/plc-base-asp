using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlcBase.Common.Enums;

namespace PlcBase.Models.Entities;

[Table(TableName.ADDRESS_DISTRICT)]
public class AddressDistrictEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [ForeignKey(nameof(AddressProvince))]
    [Column("province_id")]
    public int AddressProvinceId { get; set; }

    public AddressProvinceEntity AddressProvince { get; set; }

    public ICollection<AddressWardEntity> AddressWards { get; set; }
}
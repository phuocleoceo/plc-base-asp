using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlcBase.Common.Enums;

namespace PlcBase.Models.Entities;

[Table(TableName.ADDRESS_PROVINCE)]
public class AddressProvinceEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    public ICollection<AddressDistrictEntity> AddressDistricts { get; set; }
}
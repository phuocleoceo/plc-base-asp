using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlcBase.Common.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Models.Entities;

[Table(TableName.ROLE)]
public class RoleEntity : BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    public ICollection<UserAccountEntity> UserAccounts { get; set; }

    public ICollection<PermissionEntity> Permissions { get; set; }
}
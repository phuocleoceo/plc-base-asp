using System.ComponentModel.DataAnnotations.Schema;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Models.Entities;

[Table(TableName.ROLE)]
public class RoleEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    public ICollection<UserAccountEntity> UserAccounts { get; set; }

    public ICollection<PermissionEntity> Permissions { get; set; }
}
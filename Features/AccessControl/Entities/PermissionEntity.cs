using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.AccessControl.Entities;

[Table(TableName.PERMISSION)]
public class PermissionEntity : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [ForeignKey(nameof(Role))]
    [Column("role_id")]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; }
}
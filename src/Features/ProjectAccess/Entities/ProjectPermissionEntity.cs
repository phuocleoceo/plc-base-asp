using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectAccess.Entities;

[Table(TableName.PROJECT_PERMISSION)]
public class ProjectPermissionEntity : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [ForeignKey(nameof(Role))]
    [Column("role_id")]
    public int RoleId { get; set; }
    public ProjectRoleEntity Role { get; set; }
}

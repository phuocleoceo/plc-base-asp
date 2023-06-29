using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectAccess.Entities;

[Table(TableName.PROJECT_PERMISSION)]
public class ProjectPermissionEntity : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }

    [ForeignKey(nameof(ProjectRole))]
    [Column("project_role_id")]
    public int ProjectRoleId { get; set; }
    public ProjectRoleEntity ProjectRole { get; set; }
}

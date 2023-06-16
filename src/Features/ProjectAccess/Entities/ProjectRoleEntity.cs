using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectAccess.Entities;

[Table(TableName.PROJECT_ROLE)]
public class ProjectRoleEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }
}

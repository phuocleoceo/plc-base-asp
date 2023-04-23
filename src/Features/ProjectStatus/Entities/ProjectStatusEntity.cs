using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.Project.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectStatus.Entities;

[Table(TableName.PROJECT_STATUS)]
public class ProjectStatusEntity : BaseSoftDeletableEntity
{
    [Column("name")]
    public string Name { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
}

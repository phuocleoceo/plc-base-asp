using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.Project.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Sprint.Entities;

[Table(TableName.SPRINT)]
public class SprintEntity : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }

    [Column("goal")]
    public string Goal { get; set; }

    [Column("start_time")]
    public DateTime? StartTime { get; set; }

    [Column("end_time")]
    public DateTime? EndTime { get; set; }

    [Column("is_in_progress")]
    public bool IsInProgress { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
}

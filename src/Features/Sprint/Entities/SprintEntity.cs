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

    [Column("from_date")]
    public DateTime? FromDate { get; set; }

    [Column("to_date")]
    public DateTime? ToDate { get; set; }

    [Column("started_at")]
    public DateTime? StartedAt { get; set; }

    [Column("completed_at")]
    public DateTime? CompletedAt { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
}

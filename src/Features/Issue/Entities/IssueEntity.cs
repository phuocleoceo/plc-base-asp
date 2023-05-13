using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Issue.Entities;

[Table(TableName.ISSUE)]
public class IssueEntity : BaseSoftDeletableEntity
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("story_point")]
    public double StoryPoint { get; set; }

    [Column("priority")]
    public string Priority { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("backlog_index")]
    public double? BacklogIndex { get; set; }

    [ForeignKey(nameof(Sprint))]
    [Column("sprint_id")]
    public int? SprintId { get; set; }
    public SprintEntity Sprint { get; set; }

    [ForeignKey(nameof(Reporter))]
    [Column("reporter_id")]
    public int ReporterId { get; set; }
    public UserAccountEntity Reporter { get; set; }

    [ForeignKey(nameof(Assignee))]
    [Column("assignee_id")]
    public int? AssigneeId { get; set; }
    public UserAccountEntity Assignee { get; set; }

    [ForeignKey(nameof(ProjectStatus))]
    [Column("project_status_id")]
    public int? ProjectStatusId { get; set; }
    public ProjectStatusEntity ProjectStatus { get; set; }

    [Column("project_status_index")]
    public double? ProjectStatusIndex { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
}

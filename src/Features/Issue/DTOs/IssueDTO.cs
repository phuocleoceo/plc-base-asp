namespace PlcBase.Features.Issue.DTOs;

public class IssueDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double StoryPoint { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }
    public double? BacklogIndex { get; set; }
    public int? SprintId { get; set; }

    public int ReporterId { get; set; }
    public string ReporterName { get; set; }
    public string ReporterAvatar { get; set; }

    public int? AssigneeId { get; set; }
    public string AssigneeName { get; set; }
    public string AssigneeAvatar { get; set; }

    public int? ProjectStatusId { get; set; }
    public string ProjectStatusName { get; set; }
}

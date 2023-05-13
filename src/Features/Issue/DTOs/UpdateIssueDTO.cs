namespace PlcBase.Features.Issue.DTOs;

public class UpdateIssueDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double StoryPoint { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }
    public int ReporterId { get; set; }
    public int? AssigneeId { get; set; }
    public int? ProjectStatusId { get; set; }
    public int? SprintId { get; set; }
}

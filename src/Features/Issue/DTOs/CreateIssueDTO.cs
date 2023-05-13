namespace PlcBase.Features.Issue.DTOs;

public class CreateIssueDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double StoryPoint { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }
    public double? BacklogIndex { get; set; }
    public int? SprintId { get; set; }
    public int? AssigneeId { get; set; }
}

namespace PlcBase.Features.Issue.DTOs;

public class IssueBacklogDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double StoryPoint { get; set; }
    public string Priority { get; set; }
    public string Type { get; set; }
    public double? BacklogIndex { get; set; }

    public int? AssigneeId { get; set; }
    public string AssigneeName { get; set; }
    public string AssigneeAvatar { get; set; }
}

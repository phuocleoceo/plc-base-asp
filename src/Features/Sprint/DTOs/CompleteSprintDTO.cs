namespace PlcBase.Features.Sprint.DTOs;

public class CompleteSprintDTO
{
    public List<int> CompletedIssues { get; set; }

    public List<int> UnCompletedIssues { get; set; }

    // backlog or next_sprint
    public string MoveType { get; set; }
}

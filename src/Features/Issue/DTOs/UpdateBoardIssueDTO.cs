namespace PlcBase.Features.Issue.DTOs;

public class UpdateBoardIssueDTO
{
    public int? SprintId { get; set; }

    public int? ProjectStatusId { get; set; }

    public double? ProjectStatusIndex { get; set; }
}

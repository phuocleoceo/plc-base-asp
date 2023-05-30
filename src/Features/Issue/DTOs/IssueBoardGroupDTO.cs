namespace PlcBase.Features.Issue.DTOs;

public class IssueBoardGroupDTO
{
    public int ProjectStatusId { get; set; }

    public IEnumerable<IssueBoardDTO> Issues { get; set; }
}

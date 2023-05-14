namespace PlcBase.Features.Sprint.DTOs;

public class CreateSprintDTO
{
    public string Title { get; set; }
    public string Goal { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

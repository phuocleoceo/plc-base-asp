namespace PlcBase.Features.Sprint.DTOs;

public class UpdateSprintDTO
{
    public string Title { get; set; }
    public string Goal { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

namespace PlcBase.Features.Event.DTOs;

public class UpdateEventDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public HashSet<int> AttendeeIds { get; set; }
}

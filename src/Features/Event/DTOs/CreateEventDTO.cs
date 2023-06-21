namespace PlcBase.Features.Event.DTOs;

public class CreateEventDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<int> AttendeeIds { get; set; }
}

namespace PlcBase.Features.Sprint.DTOs;

public class CreateSprintDTO
{
    public string Title { get; set; }
    public string Goal { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

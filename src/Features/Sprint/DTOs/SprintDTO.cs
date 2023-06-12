namespace PlcBase.Features.Sprint.DTOs;

public class SprintDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Goal { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

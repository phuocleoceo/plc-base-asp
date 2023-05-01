namespace PlcBase.Features.ProjectStatus.DTOs;

public class UpdateStatusIndexDTO
{
    public List<int> StatusIds { get; set; }

    public List<int> NewIndexes { get; set; }
}

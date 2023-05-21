namespace PlcBase.Features.Project.DTOs;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Key { get; set; }
    public int CreatorId { get; set; }

    public int LeaderId { get; set; }
    public string LeaderName { get; set; }
    public string LeaderAvatar { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

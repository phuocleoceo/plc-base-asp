namespace PlcBase.Features.Issue.DTOs;

public class IssueCommentDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string Content { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserAvatar { get; set; }
}

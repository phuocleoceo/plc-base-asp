namespace PlcBase.Features.Invitation.DTOs;

public class RecipientInvitationDTO
{
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string SenderAvatar { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? DeclinedAt { get; set; }
}

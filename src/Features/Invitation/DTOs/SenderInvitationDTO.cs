namespace PlcBase.Features.Invitation.DTOs;

public class SenderInvitationDTO
{
    public int RecipientId { get; set; }
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? DeclinedAt { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.Project.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Invitation.Entities;

[Table(TableName.INVITATION)]
public class InvitationEntity : BaseEntity
{
    [ForeignKey(nameof(Recipient))]
    [Column("recipient_id")]
    public int RecipientId { get; set; }
    public UserAccountEntity Recipient { get; set; }

    [ForeignKey(nameof(Sender))]
    [Column("sender_id")]
    public int SenderId { get; set; }
    public UserAccountEntity Sender { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }

    [Column("accepted")]
    public bool Accepted { get; set; }

    [Column("declined")]
    public bool Declined { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Event.Entities;

[Table(TableName.EVENT_ATTENDEE)]
public class EventAttendeeEntity : BaseEntity
{
    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserAccountEntity User { get; set; }

    [ForeignKey(nameof(Event))]
    [Column("event_id")]
    public int EventId { get; set; }
    public EventEntity Event { get; set; }
}

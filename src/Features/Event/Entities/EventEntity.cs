using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.Project.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Event.Entities;

[Table(TableName.EVENT)]
public class EventEntity : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    public DateTime EndTime { get; set; }

    [ForeignKey(nameof(Creator))]
    [Column("creator_id")]
    public int CreatorId { get; set; }
    public UserAccountEntity Creator { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }

    public ICollection<EventAttendeeEntity> Attendees { get; set; }

    public EventEntity()
    {
        Attendees = new List<EventAttendeeEntity>();
    }
}

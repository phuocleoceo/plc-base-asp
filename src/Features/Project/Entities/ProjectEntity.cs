using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Project.Entities;

[Table(TableName.PROJECT)]
public class ProjectEntity : BaseSoftDeletableEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("image")]
    public string Image { get; set; }

    [Column("key")]
    public string Key { get; set; }

    [ForeignKey(nameof(Creator))]
    [Column("creator_id")]
    public int CreatorId { get; set; }
    public UserAccountEntity Creator { get; set; }

    [ForeignKey(nameof(Leader))]
    [Column("leader_id")]
    public int LeaderId { get; set; }
    public UserAccountEntity Leader { get; set; }
}

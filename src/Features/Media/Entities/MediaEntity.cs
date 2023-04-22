using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Media.Entities;

[Table(TableName.MEDIA)]
public class MediaEntity : BaseEntity
{
    [Column("content_type")]
    public string ContentType { get; set; }

    [Column("url")]
    public string Url { get; set; }

    [Column("entity_type")]
    public string EntityType { get; set; }

    [Column("entity_id")]
    public int EntityId { get; set; }
}

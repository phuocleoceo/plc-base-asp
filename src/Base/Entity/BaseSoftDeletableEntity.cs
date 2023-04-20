using System.ComponentModel.DataAnnotations.Schema;

namespace PlcBase.Base.Entity;

public abstract class BaseSoftDeletableEntity : BaseEntity, ISoftDeletable
{
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}

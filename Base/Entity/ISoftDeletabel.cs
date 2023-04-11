using System.ComponentModel.DataAnnotations.Schema;

namespace PlcBase.Base.Entity;

public interface ISoftDeletable
{
    [Column("deleted_at")]
    DateTime? DeletedAt { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ConfigSetting.Entities;

[Table(TableName.CONFIG_SETTING)]
public class ConfigSettingEntity : BaseEntity
{
    [Column("key")]
    public string Key { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("value")]
    public double Value { get; set; }
}

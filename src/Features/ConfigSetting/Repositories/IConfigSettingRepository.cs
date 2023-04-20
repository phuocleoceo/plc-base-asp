using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ConfigSetting.Repositories;

public interface IConfigSettingRepository : IBaseRepository<ConfigSettingEntity>
{
    Task<ConfigSettingEntity> GetByKey(string key);
}

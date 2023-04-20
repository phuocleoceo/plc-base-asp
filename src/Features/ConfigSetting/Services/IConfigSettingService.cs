using PlcBase.Features.ConfigSetting.DTOs;

namespace PlcBase.Features.ConfigSetting.Services;

public interface IConfigSettingService
{
    Task<List<ConfigSettingDTO>> GetAllConfigSettings();

    Task<ConfigSettingDTO> GetByKey(string key);

    Task<double> GetValueByKey(string key);

    Task<bool> UpdateForKey(string key, ConfigSettingUpdateDTO configSettingUpdateDTO);
}

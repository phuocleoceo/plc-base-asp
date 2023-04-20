using AutoMapper;

using PlcBase.Features.ConfigSetting.Entities;

namespace PlcBase.Features.ConfigSetting.DTOs;

public class ConfigSettingMapping : Profile
{
    public ConfigSettingMapping()
    {
        CreateMap<ConfigSettingEntity, ConfigSettingDTO>();

        CreateMap<ConfigSettingUpdateDTO, ConfigSettingEntity>();
    }
}

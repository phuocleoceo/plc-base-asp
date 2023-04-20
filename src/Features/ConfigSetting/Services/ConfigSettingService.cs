using AutoMapper;

using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.ConfigSetting.Services;

public class ConfigSettingService : IConfigSettingService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ConfigSettingService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<ConfigSettingDTO>> GetAllConfigSettings()
    {
        return await _uow.ConfigSetting.GetManyAsync<ConfigSettingDTO>();
    }

    public async Task<ConfigSettingDTO> GetByKey(string key)
    {
        return await _uow.ConfigSetting.GetOneAsync<ConfigSettingDTO>(
            new QueryModel<ConfigSettingEntity>() { Filters = { cf => cf.Key == key } }
        );
    }

    public async Task<double> GetValueByKey(string key)
    {
        return (await _uow.ConfigSetting.GetByKey(key)).Value;
    }

    public async Task<bool> UpdateForKey(string key, ConfigSettingUpdateDTO configSettingUpdateDTO)
    {
        ConfigSettingEntity configSettingDB = await _uow.ConfigSetting.GetByKey(key);
        _mapper.Map(configSettingUpdateDTO, configSettingDB);

        _uow.ConfigSetting.Update(configSettingDB);
        return await _uow.Save() > 0;
    }
}

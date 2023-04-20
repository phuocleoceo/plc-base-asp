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

    public Task<ConfigSettingDTO> GetByKey(string key)
    {
        throw new NotImplementedException();
    }

    public Task<double> GetValueByKey(string key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateForKey(string key, ConfigSettingUpdateDTO configSettingUpdateDTO)
    {
        throw new NotImplementedException();
    }
}

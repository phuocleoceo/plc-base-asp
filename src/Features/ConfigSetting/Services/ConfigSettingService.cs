using AutoMapper;

using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;

namespace PlcBase.Features.ConfigSetting.Services;

public class ConfigSettingService : IConfigSettingService
{
    private readonly IRedisHelper _redisHelper;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ConfigSettingService(IUnitOfWork uow, IMapper mapper, IRedisHelper redisHelper)
    {
        _uow = uow;
        _mapper = mapper;
        _redisHelper = redisHelper;
    }

    public async Task<List<ConfigSettingDTO>> GetAllConfigSettings()
    {
        return await _redisHelper.GetCachedOr(
            RedisUtility.GetListKey<ConfigSettingDTO>(),
            async () => await _uow.ConfigSetting.GetManyAsync<ConfigSettingDTO>()
        );
    }

    public async Task<ConfigSettingDTO> GetByKey(string key)
    {
        return await _redisHelper.GetCachedOr(
            RedisUtility.GetKey<ConfigSettingDTO>(key),
            async () =>
                await _uow.ConfigSetting.GetOneAsync<ConfigSettingDTO>(
                    new QueryModel<ConfigSettingEntity>() { Filters = { cf => cf.Key == key } }
                )
        );
    }

    public async Task<bool> UpdateForKey(string key, ConfigSettingUpdateDTO configSettingUpdateDTO)
    {
        ConfigSettingEntity configSettingDb = await _uow.ConfigSetting.GetByKey(key);
        if (configSettingDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "config_setting_not_found");

        _mapper.Map(configSettingUpdateDTO, configSettingDb);
        _uow.ConfigSetting.Update(configSettingDb);

        await _redisHelper.ClearByPattern(RedisUtility.GetClearKey<ConfigSettingDTO>());
        return await _uow.Save();
    }
}

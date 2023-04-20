using AutoMapper;

using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ConfigSetting.Repositories;

public class ConfigSettingRepository : BaseRepository<ConfigSettingEntity>, IConfigSettingRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ConfigSettingRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ConfigSettingEntity> GetByKey(string key)
    {
        return await GetOneAsync<ConfigSettingEntity>(
            new QueryModel<ConfigSettingEntity>() { Filters = { cf => cf.Key == key } }
        );
    }
}

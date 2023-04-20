using AutoMapper;

using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Common.Data.Context;
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
}

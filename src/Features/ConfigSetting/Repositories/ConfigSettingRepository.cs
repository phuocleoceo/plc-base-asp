using AutoMapper;

using PlcBase.Features.Media.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Media.Repositories;

public class MediaRepository : BaseRepository<MediaEntity>, IMediaRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public MediaRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}

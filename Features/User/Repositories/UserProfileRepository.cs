using AutoMapper
;
using PlcBase.Features.User.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.User.Repositories;

public class UserProfileRepository : BaseRepository<UserProfileEntity>, IUserProfileRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public UserProfileRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
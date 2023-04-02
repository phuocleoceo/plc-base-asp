using PlcBase.Features.User.Entities;
using PlcBase.Shared.Data.Context;
using PlcBase.Base.Repository;
using AutoMapper;

namespace PlcBase.Features.User.Repositories;

public class UserAccountRepository : BaseRepository<UserAccountEntity>, IUserAccountRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public UserAccountRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}


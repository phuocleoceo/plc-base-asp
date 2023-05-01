using AutoMapper;

using PlcBase.Features.User.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Repository;

namespace PlcBase.Features.User.Repositories;

public class UserAccountRepository : BaseRepository<UserAccountEntity>, IUserAccountRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public UserAccountRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<UserAccountEntity> FindByEmail(string email)
    {
        return await GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>() { Filters = { ua => ua.Email == email } }
        );
    }
}

using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class UserAccountRepository : BaseRepository<UserAccountEntity>, IUserAccountRepository
{
    private readonly DataContext _db;

    public UserAccountRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}


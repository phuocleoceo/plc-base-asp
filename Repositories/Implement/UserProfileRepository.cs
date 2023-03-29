using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class UserProfileRepository : BaseRepository<UserProfileEntity>, IUserProfileRepository
{
    private readonly DataContext _db;

    public UserProfileRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}
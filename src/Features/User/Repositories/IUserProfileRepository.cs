using PlcBase.Features.User.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.User.Repositories;

public interface IUserProfileRepository : IBaseRepository<UserProfileEntity>
{
    Task<UserProfileEntity> GetProfileByAccountId(int accountId);

    Task<bool> MakePayment(int userId, double amount);
}

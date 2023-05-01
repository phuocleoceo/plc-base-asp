using PlcBase.Features.User.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.User.Repositories;

public interface IUserAccountRepository : IBaseRepository<UserAccountEntity>
{
    Task<UserAccountEntity> FindByEmail(string email);
}

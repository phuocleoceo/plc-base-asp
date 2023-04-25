using PlcBase.Base.DTO;
using PlcBase.Features.User.DTOs;

namespace PlcBase.Features.User.Services;

public interface IUserService
{
    Task<PagedList<UserDTO>> GetAllUsers(UserParams userParams);
}

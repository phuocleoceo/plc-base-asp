using PlcBase.Features.User.DTOs;
using PlcBase.Base.DomainModel;
using PlcBase.Base.DTO;

namespace PlcBase.Features.User.Services;

public interface IUserService
{
    Task<PagedList<UserDTO>> GetAllUsers(UserParams userParams);

    Task<UserProfilePersonalDTO> GetUserProfilePersonal(ReqUser reqUser);

    Task<UserProfileAnonymousDTO> GetUserProfileAnonymous(int userId);

    Task<bool> UpdateUserProfile(ReqUser reqUser, UserProfileUpdateDTO userProfileUpdateDTO);

    Task<UserAccountDTO> GetUserAccountById(int userId);

    Task<bool> UpdateUserAccount(int userId, UserAccountUpdateDTO userAccountUpdateDTO);
}

using PlcBase.Base.DomainModel;
using PlcBase.Models.DTO;

namespace PlcBase.Features.Auth.Services;

public interface IAuthService
{
    Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);

    Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO);

    Task<bool> ConfirmEmail(UserConfirmEmailDTO userConfirmEmailDTO);

    Task<bool> ChangePassword(ReqUser reqUser, UserChangePasswordDTO userChangePasswordDTO);

    Task ForgotPassword(UserForgotPasswordDTO userForgotPasswordDTO);

    Task<bool> RecoverPassword(UserRecoverPasswordDTO userRecoverPasswordDTO);
}
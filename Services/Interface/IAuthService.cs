using PlcBase.Base.DomainModel;
using PlcBase.Models.DTO;

namespace PlcBase.Services.Interface;

public interface IAuthService
{
    Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);

    Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO);

    Task<bool> ConfirmEmail(UserConfirmEmailDTO userConfirmEmailDTO);

    Task<bool> ChangePassword(ReqUser reqUser, UserChangePasswordDTO userChangePasswordDTO);

    Task ForgotPassword(UserForgotPasswordDTO userForgotPasswordDTO);

    // Task RecoverPassword(UserRecoverPasswordDTO userRecoverPasswordDTO);
}
using PlcBase.Models.DTO;

namespace PlcBase.Services.Interface;

public interface IAuthService
{
    Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);

    Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO);

    // Task<bool> ConfirmEmail(UserConfirmEmailDTO userConfirmEmailDTO);

    // Task<bool> ChangePassword(int userId, UserChangePasswordDTO userChangePasswordDTO);

    // Task ForgotPassword(string email);

    // Task RecoverPassword(UserRecoverPasswordDTO userRecoverPasswordDTO);
}
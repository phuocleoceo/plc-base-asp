using PlcBase.Models.DTO;

namespace PlcBase.Services.Interface;

public interface IAuthService
{
    Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);

    Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO);
}
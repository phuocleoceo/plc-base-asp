using PlcBase.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Models.DTO;
using PlcBase.Base.DTO;

namespace PlcBase.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<BaseResponse<UserLoginResponseDTO>> Login([FromBody] UserLoginDTO userLoginDTO)
    {
        UserLoginResponseDTO loginResponse = await _authService.Login(userLoginDTO);
        return HttpContext.Success(loginResponse);
    }

    [HttpPost("Register")]
    public async Task<BaseResponse<UserRegisterResponseDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
    {
        UserRegisterResponseDTO registerResponse = await _authService.Register(userRegisterDTO);
        return HttpContext.Success(registerResponse);
    }

    // [HttpPut("Confirm-Email")]
    // public async Task<BaseResponse<object>> ConfirmEmail()
    // {
    // }

    // [HttpPut("Change-Password")]
    // public async Task<BaseResponse<object>> ChangePassword()
    // {
    // }

    // [HttpGet("Forgot-Password")]
    // public async Task<BaseResponse<object>> ForgotPassword()
    // {
    // }

    // [HttpPut("Recover-Password")]
    // public async Task<BaseResponse<object>> RecoverPassword()
    // {
    // }
}
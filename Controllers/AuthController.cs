using Microsoft.AspNetCore.Authorization;
using PlcBase.Extensions.Utilities;
using PlcBase.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.DomainModel;
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
        return HttpContext.Success(await _authService.Login(userLoginDTO));
    }

    [HttpPost("Register")]
    public async Task<BaseResponse<UserRegisterResponseDTO>> Register([FromBody] UserRegisterDTO userRegisterDTO)
    {
        return HttpContext.Success(await _authService.Register(userRegisterDTO));
    }

    [HttpPut("Confirm-Email")]
    public async Task<BaseResponse<bool>> ConfirmEmail([FromBody] UserConfirmEmailDTO userConfirmEmailDTO)
    {
        if (await _authService.ConfirmEmail(userConfirmEmailDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("Change-Password")]
    [Authorize]
    public async Task<BaseResponse<bool>> ChangePassword([FromBody] UserChangePasswordDTO userChangePasswordDTO)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _authService.ChangePassword(reqUser, userChangePasswordDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    // [HttpGet("Forgot-Password")]
    // public async Task<BaseResponse<object>> ForgotPassword()
    // {
    // }

    // [HttpPut("Recover-Password")]
    // public async Task<BaseResponse<object>> RecoverPassword()
    // {
    // }
}
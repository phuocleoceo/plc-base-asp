using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Auth.Services;
using PlcBase.Features.Auth.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Auth.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<SuccessResponse<UserLoginResponseDTO>> Login(
        [FromBody] UserLoginDTO userLoginDTO
    )
    {
        return HttpContext.Success(await _authService.Login(userLoginDTO));
    }

    [HttpPost("Register")]
    public async Task<SuccessResponse<UserRegisterResponseDTO>> Register(
        [FromBody] UserRegisterDTO userRegisterDTO
    )
    {
        return HttpContext.Success(await _authService.Register(userRegisterDTO));
    }

    [HttpPut("Confirm-Email")]
    public async Task<SuccessResponse<bool>> ConfirmEmail(
        [FromBody] UserConfirmEmailDTO userConfirmEmailDTO
    )
    {
        if (await _authService.ConfirmEmail(userConfirmEmailDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPut("Change-Password")]
    [Authorize]
    public async Task<SuccessResponse<bool>> ChangePassword(
        [FromBody] UserChangePasswordDTO userChangePasswordDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _authService.ChangePassword(reqUser, userChangePasswordDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPost("Forgot-Password")]
    public async Task<SuccessResponse<bool>> ForgotPassword(
        [FromBody] UserForgotPasswordDTO userForgotPasswordDTO
    )
    {
        await _authService.ForgotPassword(userForgotPasswordDTO);
        return HttpContext.Success(true);
    }

    [HttpPut("Recover-Password")]
    public async Task<SuccessResponse<bool>> RecoverPassword(
        UserRecoverPasswordDTO userRecoverPasswordDTO
    )
    {
        if (await _authService.RecoverPassword(userRecoverPasswordDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpPost("Refresh-Token")]
    public async Task<SuccessResponse<UserRefreshTokenResponseDTO>> RefreshToken(
        [FromBody] UserRefreshTokenDTO userRefreshTokenDTO
    )
    {
        return HttpContext.Success(await _authService.RefreshToken(userRefreshTokenDTO));
    }

    [HttpPost("Revoke-Refresh-Token")]
    [Authorize]
    public async Task<SuccessResponse<bool>> RevokeRefreshToken()
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _authService.RevokeRefreshToken(reqUser))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

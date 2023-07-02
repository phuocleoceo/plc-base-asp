using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.User.Services;
using PlcBase.Features.User.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Shared.Enums;
using PlcBase.Base.DTO;

namespace PlcBase.Features.User.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<PagedList<UserDTO>>> GetAllUsers(
        [FromQuery] UserParams userParams
    )
    {
        return HttpContext.Success(await _userService.GetAllUsers(userParams));
    }

    [HttpGet("Personal")]
    [Authorize]
    public async Task<BaseResponse<UserProfilePersonalDTO>> GetUserProfilePersonal()
    {
        ReqUser reqUser = HttpContext.GetRequestUser();
        return HttpContext.Success(await _userService.GetUserProfilePersonal(reqUser));
    }

    [HttpPut("Personal")]
    [Authorize]
    public async Task<BaseResponse<bool>> UpdateUserProfilePersonal(
        [FromBody] UserProfileUpdateDTO userProfileUpdateDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _userService.UpdateUserProfile(reqUser, userProfileUpdateDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }

    [HttpGet("Anonymous/{userId}")]
    [Authorize]
    public async Task<BaseResponse<UserProfileAnonymousDTO>> GetUserProfileAnonymous(int userId)
    {
        return HttpContext.Success(await _userService.GetUserProfileAnonymous(userId));
    }

    [HttpGet("Account/{userId}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<UserAccountDTO>> GetUserAccountById(int userId)
    {
        return HttpContext.Success(await _userService.GetUserAccountById(userId));
    }

    [HttpPut("Account/{userId}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<bool>> UpdateUserAccount(
        int userId,
        [FromBody] UserAccountUpdateDTO userAccountUpdateDTO
    )
    {
        if (await _userService.UpdateUserAccount(userId, userAccountUpdateDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

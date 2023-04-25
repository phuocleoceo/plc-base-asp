using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.User.Services;
using PlcBase.Features.User.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
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
    public async Task<BaseResponse<PagedList<UserDTO>>> GetAllUsers(
        [FromQuery] UserParams userParams
    )
    {
        return HttpContext.Success(await _userService.GetAllUsers(userParams));
    }

    [HttpGet("Personal")]
    public async Task<BaseResponse<UserProfilePersonalDTO>> GetUserProfilePersonal()
    {
        ReqUser reqUser = HttpContext.GetRequestUser();
        return HttpContext.Success(await _userService.GetUserProfilePersonal(reqUser));
    }
}

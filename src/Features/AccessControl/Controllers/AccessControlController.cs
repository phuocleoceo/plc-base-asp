using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.AccessControl.Services;
using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.AccessControl.Controllers;

public class AccessControlController : BaseController
{
    private readonly IAccessControlService _accessControlService;

    public AccessControlController(IAccessControlService accessControlService)
    {
        _accessControlService = accessControlService;
    }

    [HttpGet("/api/roles")]
    public async Task<BaseResponse<List<RoleDTO>>> GetProvinces()
    {
        return HttpContext.Success(await _accessControlService.GetRoles());
    }
}

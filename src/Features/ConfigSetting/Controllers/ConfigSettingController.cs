using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ConfigSetting.Services;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Shared.Enums;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ConfigSetting.Controllers;

[Route("api/config-setting")]
public class ConfigSettingController : BaseController
{
    private readonly IConfigSettingService _configSettingService;

    public ConfigSettingController(IConfigSettingService configSettingService)
    {
        _configSettingService = configSettingService;
    }

    [HttpGet("")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<List<ConfigSettingDTO>>> GetAllConfigSettings()
    {
        return HttpContext.Success(await _configSettingService.GetAllConfigSettings());
    }

    [HttpGet("{key}")]
    [Authorize]
    public async Task<BaseResponse<ConfigSettingDTO>> GetConfigSettingByKey(string key)
    {
        return HttpContext.Success(await _configSettingService.GetByKey(key));
    }

    [HttpPut("{key}")]
    [Authorize(Roles = AppRole.ADMIN)]
    public async Task<BaseResponse<bool>> UpdateConfigSetting(
        string key,
        ConfigSettingUpdateDTO configSettingUpdateDTO
    )
    {
        if (await _configSettingService.UpdateForKey(key, configSettingUpdateDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}

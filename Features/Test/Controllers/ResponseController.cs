using static PlcBase.Shared.Enums.PermissionPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Shared.Constants;
using PlcBase.Base.Controller;
using PlcBase.Base.Authorize;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Test.Controllers;

public class ResponseController : BaseController
{
    public ResponseController() { }

    [HttpGet("OK")]
    public BaseResponse<string> ResponseOK()
    {
        return HttpContext.Success("ok", HttpCode.OK);
    }

    [HttpGet("Created")]
    public BaseResponse<string> ResponseCreated()
    {
        return HttpContext.Success("created", HttpCode.CREATED, "create_successfully");
    }

    [HttpGet("Internal-Server-Error")]
    public BaseResponse<string> ResponseInternalServerError()
    {
        return HttpContext.Failure(HttpCode.INTERNAL_SERVER_ERROR, ErrorMessage.SERVER_ERROR);
    }

    [HttpGet("Unauthorized")]
    [Authorize]
    public BaseResponse<string> ResponseUnauthorized()
    {
        return HttpContext.Failure(HttpCode.UNAUTHORIZED, ErrorMessage.UNAUTHORIZED_USER);
    }

    [HttpGet("Forbidden")]
    [PermissionAuthorize(CommonPermission.Basic)]
    public BaseResponse<string> ResponseForbidden()
    {
        return HttpContext.Failure(HttpCode.FORBIDDEN, ErrorMessage.FORBIDDEN_RESOURCE);
    }
}
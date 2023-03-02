using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Common.Constants;
using PlcBase.Base.Controller;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Controllers;

public class ResponseController : BaseController
{
    public ResponseController() { }

    [HttpGet("OK")]
    public BaseResponse<string> ResponseOK()
    {
        return new BaseResponse<string>("OK");
    }

    [HttpGet("Internal-Server-Error")]
    public BaseResponse<string> ResponseInternalServerError()
    {
        throw new BaseException(HttpCode.INTERNAL_SERVER_ERROR, "internal_server_error");
    }

    [HttpGet("Unauthorized")]
    [Authorize]
    public BaseResponse<string> ResponseUnauthorized()
    {
        throw new BaseException(HttpCode.UNAUTHORIZED, ErrorMessage.UNAUTHORIZED_USER);
    }

    [HttpGet("Forbidden")]
    public BaseResponse<string> ResponseForbidden()
    {
        throw new BaseException(HttpCode.FORBIDDEN, ErrorMessage.FORBIDDEN_RESOURCE);
    }
}
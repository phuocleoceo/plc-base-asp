using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;
using PlcBase.Helpers;

namespace PlcBase.Controllers;

public class HelperController : BaseController
{
    private readonly ISendMailHelper _sendMailHelper;

    public HelperController(ISendMailHelper sendMailHelper)
    {
        _sendMailHelper = sendMailHelper;
    }

    [HttpPost("Send-Mail")]
    public async Task<BaseResponse<string>> Send([FromForm] MailContent mailContent)
    {
        string mailTo = await _sendMailHelper.SendEmailAsync(mailContent);
        return HttpContext.Success(new BaseResponse<string>(mailTo));
    }
}
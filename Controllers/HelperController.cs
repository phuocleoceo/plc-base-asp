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
        await _sendMailHelper.SendEmailAsync(mailContent);
        return new BaseResponse<string>($"Sent mail to {mailContent.ToEmail}");
    }
}
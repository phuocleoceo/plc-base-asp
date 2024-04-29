using Microsoft.AspNetCore.Mvc;
using DotNetCore.CAP;

using PlcBase.Base.Controller;
using PlcBase.Shared.Helpers;
using PlcBase.Shared.Enums;

namespace PlcBase.Features.Worker.Controllers;

public class WorkerController : BaseController
{
    private readonly ISendMailHelper _sendMailHelper;

    public WorkerController(ISendMailHelper sendMailHelper)
    {
        _sendMailHelper = sendMailHelper;
    }

    [NonAction]
    [CapSubscribe(WorkerType.SEND_MAIL)]
    public async Task SendMail(MailContent mailContent)
    {
        await _sendMailHelper.SendEmailAsync(mailContent);
    }
}

using Microsoft.AspNetCore.Mvc;

using PlcBase.Shared.Utilities;
using PlcBase.Base.Controller;
using PlcBase.Shared.Helpers;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Test.Controllers;

public class HelperController : BaseController
{
    private readonly ISendMailHelper _sendMailHelper;
    private readonly IS3Helper _s3Helper;

    public HelperController(ISendMailHelper sendMailHelper, IS3Helper s3Helper)
    {
        _sendMailHelper = sendMailHelper;
        _s3Helper = s3Helper;
    }

    [HttpGet("Ip-Address")]
    public BaseResponse<string> GetIpAddress()
    {
        string ipAddress = HttpContext.GetIpAddress();
        return HttpContext.Success(ipAddress);
    }

    [HttpPost("Send-Mail")]
    public async Task<BaseResponse<string>> Send([FromForm] MailContent mailContent)
    {
        string mailTo = await _sendMailHelper.SendEmailAsync(mailContent);
        return HttpContext.Success(mailTo);
    }

    [HttpPost("S3-Upload")]
    public async Task<BaseResponse<string>> S3Upload(IFormFile file, string prefix = "")
    {
        string fileUrl = await _s3Helper.UploadFile(file.GetS3FileUpload());
        return HttpContext.Success(fileUrl);
    }
}

using PlcBase.Extensions.DataHandler;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;
using PlcBase.Helpers;

namespace PlcBase.Controllers;

public class HelperController : BaseController
{
    private readonly ISendMailHelper _sendMailHelper;
    private readonly IS3Helper _s3Helper;

    public HelperController(ISendMailHelper sendMailHelper,
                            IS3Helper s3Helper)
    {
        _sendMailHelper = sendMailHelper;
        _s3Helper = s3Helper;
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
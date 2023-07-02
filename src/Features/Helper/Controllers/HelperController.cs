using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Shared.Utilities;
using PlcBase.Base.Controller;
using PlcBase.Shared.Helpers;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Helper.Controllers;

[Route("api")]
public class HelperController : BaseController
{
    private readonly IS3Helper _s3Helper;

    public HelperController(IS3Helper s3Helper)
    {
        _s3Helper = s3Helper;
    }

    [HttpPost("upload-file")]
    [Authorize]
    public async Task<BaseResponse<string>> S3Upload(IFormFile file, string prefix = "")
    {
        string fileUrl = await _s3Helper.UploadFile(file.GetS3FileUpload());
        return HttpContext.Success(fileUrl);
    }
}

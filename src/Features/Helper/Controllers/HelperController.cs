using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Helper.DTOs;
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
    public async Task<SuccessResponse<string>> S3Upload(IFormFile file, string prefix = "")
    {
        return HttpContext.Success(await _s3Helper.UploadFile(file.GetS3FileUpload(prefix)));
    }

    [HttpPost("presigned-upload-url")]
    [Authorize]
    public async Task<SuccessResponse<S3PresignedUrlResponse>> GetS3PresignedUploadUrl(
        S3PresignedUrlRequest request
    )
    {
        return HttpContext.Success(await _s3Helper.GetPresignedUploadUrl(request));
    }
}

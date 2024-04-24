using PlcBase.Features.Helper.DTOs;

namespace PlcBase.Shared.Helpers;

public interface IS3Helper
{
    Task<string> UploadFile(S3FileUpload file);

    Task<string> GetPresignedUploadUrl(S3PresignedUrlRequest request);
}

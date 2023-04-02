using PlcBase.Shared.Helpers;

namespace PlcBase.Shared.Utilities;

public static class AWSUtility
{
    public static S3FileUpload GetS3FileUpload(this IFormFile formFile, string prefix = "")
    {
        string filePath = String.IsNullOrEmpty(prefix)
                            ? formFile.FileName
                            : $"{prefix.TrimEnd('/')}/{formFile.FileName}";

        return new S3FileUpload()
        {
            FilePath = filePath,
            FileContentType = formFile.ContentType,
            FileStream = formFile.OpenReadStream()
        };
    }
}
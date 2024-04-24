using PlcBase.Shared.Helpers;

namespace PlcBase.Shared.Utilities;

public static class AWSUtility
{
    public static S3FileUpload GetS3FileUpload(this IFormFile formFile, string prefix = "")
    {
        return new S3FileUpload()
        {
            FilePath = GetFilePath(formFile.FileName, prefix),
            FileContentType = formFile.ContentType,
            FileStream = formFile.OpenReadStream()
        };
    }

    public static string GetFilePath(string fileName, string prefix)
    {
        return string.IsNullOrEmpty(prefix)
            ? fileName
            : $"{prefix.TrimEnd('/')}/{fileName.Replace(" ", "-").Trim()}";
    }
}

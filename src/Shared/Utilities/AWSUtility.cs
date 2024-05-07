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

    public static string GetObjectKey(string filePath, AWSSettings awsSettings)
    {
        return awsSettings.CloudFront.Enable
            ? $"{awsSettings.CloudFront.Domain}/{filePath}"
            : $"https://{awsSettings.S3.Bucket}.s3.{awsSettings.Region}.amazonaws.com/{filePath}";
    }
}

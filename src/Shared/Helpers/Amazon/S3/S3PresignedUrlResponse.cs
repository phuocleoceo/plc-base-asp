namespace PlcBase.Features.Helper.DTOs;

public class S3PresignedUrlResponse
{
    public string PresignedUrl { get; set; }
    public string ObjectKey { get; set; }
}

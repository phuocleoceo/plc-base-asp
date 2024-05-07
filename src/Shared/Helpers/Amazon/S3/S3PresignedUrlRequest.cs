namespace PlcBase.Features.Helper.DTOs;

public class S3PresignedUrlRequest
{
    public string FilePath { get; set; }
    public string ContentType { get; set; }
}

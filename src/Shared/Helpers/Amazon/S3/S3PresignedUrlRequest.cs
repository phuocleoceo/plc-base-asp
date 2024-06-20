namespace PlcBase.Features.Helper.DTOs;

public class S3PresignedUrlRequest
{
    public string Prefix { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}

namespace PlcBase.Shared.Helpers;

public class AWSSettings
{
    public string AccessKey { get; set; }

    public string SecretKey { get; set; }

    public string Region { get; set; }

    public S3Settings S3 { get; set; } = new();

    public CloudFrontSettings CloudFront { get; set; } = new();
}

public class S3Settings
{
    public string Bucket { get; set; }

    public long PresignedUrlExpires { get; set; }
}

public class CloudFrontSettings
{
    public bool Enable { get; set; }

    public string Domain { get; set; }
}

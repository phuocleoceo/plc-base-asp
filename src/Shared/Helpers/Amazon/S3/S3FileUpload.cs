namespace PlcBase.Shared.Helpers;

public class S3FileUpload
{
    public string FilePath { get; set; }

    public string FileContentType { get; set; }

    public Stream FileStream { get; set; }
}

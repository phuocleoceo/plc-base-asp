namespace PlcBase.Helpers;

public interface IS3Helper
{
    Task<string> UploadFile(S3FileUpload file);
}
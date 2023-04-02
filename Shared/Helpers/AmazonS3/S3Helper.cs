using Microsoft.Extensions.Options;
using PlcBase.Shared.Constants;
using PlcBase.Base.Error;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.S3.Util;
using Amazon.S3;

namespace PlcBase.Shared.Helpers;

public class S3Helper : IS3Helper
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Settings _s3Settings;

    public S3Helper(IOptions<S3Settings> s3Settings)
    {
        _s3Settings = s3Settings.Value;
        _s3Client = SetupS3Client();
    }

    private AmazonS3Client SetupS3Client()
    {
        AmazonS3Config s3Config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
        };

        BasicAWSCredentials credentials =
                    new BasicAWSCredentials(_s3Settings.AccessKey, _s3Settings.SecretKey);

        return new AmazonS3Client(credentials, s3Config);
    }

    public async Task<string> UploadFile(S3FileUpload file)
    {
        string bucket = _s3Settings.Bucket;
        string region = _s3Settings.Region;

        bool bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucket);
        if (!bucketExists)
            throw new BaseException(HttpCode.NOT_FOUND, $"bucket_{bucket}_not_exists");

        PutObjectRequest request = new PutObjectRequest()
        {
            BucketName = bucket,
            Key = file.FilePath,
            InputStream = file.FileStream
        };

        request.Metadata.Add("Content-Type", file.FileContentType);
        await _s3Client.PutObjectAsync(request);

        return $"https://{bucket}.s3.{region}.amazonaws.com/{file.FilePath}";
    }
}
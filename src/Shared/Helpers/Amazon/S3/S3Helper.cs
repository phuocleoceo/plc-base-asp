using Microsoft.Extensions.Options;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.S3.Util;
using Amazon.S3;
using Amazon;

using PlcBase.Features.Helper.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Shared.Helpers;

public class S3Helper : IS3Helper
{
    private readonly IAmazonS3 _s3Client;
    private readonly AWSSettings _awsSettings;

    public S3Helper(IOptions<AWSSettings> s3Settings)
    {
        _awsSettings = s3Settings.Value;
        _s3Client = SetupS3Client();
    }

    private AmazonS3Client SetupS3Client()
    {
        AmazonS3Config s3Config = new AmazonS3Config()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_awsSettings.Region)
        };

        BasicAWSCredentials credentials = new BasicAWSCredentials(
            _awsSettings.AccessKey,
            _awsSettings.SecretKey
        );

        return new AmazonS3Client(credentials, s3Config);
    }

    public async Task<string> UploadFile(S3FileUpload file)
    {
        string bucket = _awsSettings.S3.Bucket;

        bool bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucket);
        if (!bucketExists)
        {
            throw new BaseException(HttpCode.NOT_FOUND, $"bucket_{bucket}_not_exists");
        }

        PutObjectRequest request = new PutObjectRequest()
        {
            BucketName = bucket,
            Key = file.FilePath,
            InputStream = file.FileStream
        };

        request.Metadata.Add("Content-Type", file.FileContentType);
        await _s3Client.PutObjectAsync(request);

        return AWSUtility.GetObjectKey(file.FilePath, _awsSettings);
    }

    public async Task<S3PresignedUrlResponse> GetPresignedUploadUrl(S3PresignedUrlRequest request)
    {
        GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest
        {
            BucketName = _awsSettings.S3.Bucket,
            Key = request.FilePath,
            Verb = HttpVerb.PUT,
            Expires = TimeUtility.Now().AddSeconds(_awsSettings.S3.PresignedUrlExpires),
            ContentType = request.ContentType
        };

        await Task.CompletedTask;
        return new S3PresignedUrlResponse()
        {
            PresignedUrl = _s3Client.GetPreSignedURL(getPreSignedUrlRequest),
            ObjectKey = AWSUtility.GetObjectKey(request.FilePath, _awsSettings)
        };
    }
}

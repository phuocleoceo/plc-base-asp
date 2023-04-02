using Amazon.Extensions.NETCore.Setup;
using PlcBase.Shared.Helpers;
using Amazon.Runtime;
using Amazon.S3;

namespace PlcBase.Extensions.ServiceCollections;

public static class AWSExtension
{
    public static void ConfigureAWS(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureS3(configuration);
    }

    private static void ConfigureS3(this IServiceCollection services, IConfiguration configuration)
    {
        S3Settings s3Settings = configuration.GetSection("AWSSettings:S3").Get<S3Settings>();

        AWSOptions awsOptions = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(s3Settings.AccessKey, s3Settings.SecretKey)
        };

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
    }
}

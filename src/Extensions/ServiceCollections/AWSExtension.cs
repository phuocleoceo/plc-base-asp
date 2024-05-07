using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;

using PlcBase.Shared.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class AWSExtension
{
    public static void ConfigureAWS(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureS3(configuration);
    }

    private static void ConfigureS3(this IServiceCollection services, IConfiguration configuration)
    {
        AWSSettings awsSettings = configuration.GetSection("AWSSettings").Get<AWSSettings>();

        AWSOptions awsOptions = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey)
        };

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
    }
}

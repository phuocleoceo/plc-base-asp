using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlcBase.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class AuthExtension
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        var publicKey = JwtOptions.GetPublicKey(tokenSettings);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = JwtOptions.GetTokenParams(tokenSettings, publicKey);
        });
    }
}
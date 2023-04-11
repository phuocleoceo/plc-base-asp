using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using PlcBase.Shared.Helpers;

namespace PlcBase.Extensions.ServiceCollections;

public static class AuthExtension
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSettings tokenSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        SecurityKey publicKey = JwtOptions.GetPublicKey(tokenSettings);
        services
            .AddAuthentication(options =>
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

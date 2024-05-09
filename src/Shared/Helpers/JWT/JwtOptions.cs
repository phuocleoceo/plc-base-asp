using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace PlcBase.Shared.Helpers;

public static class JwtOptions
{
    public static TokenValidationParameters GetTokenParams(
        JwtSettings jwtSettings,
        SecurityKey securityKey
    )
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = securityKey,
            ValidateLifetime = jwtSettings.ValidateLifetime,
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidIssuer = jwtSettings.ValidateIssuer ? jwtSettings.ValidIssuer : null,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidAudience = jwtSettings.ValidateAudience ? jwtSettings.ValidAudience : null,
            ClockSkew = TimeSpan.FromSeconds(jwtSettings.ClockSkew),
        };
    }

    public static RsaSecurityKey GetPrivateKey(string keyPath)
    {
        string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, keyPath);

        string privateKeyPem = File.ReadAllText(fileName);
        privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
        privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");
        byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

        RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
        provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
        return new RsaSecurityKey(provider);
    }

    public static RsaSecurityKey GetPublicKey(string keyPath)
    {
        string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, keyPath);
        X509Certificate2 cert = new X509Certificate2(fileName);
        return new RsaSecurityKey(cert.GetRSAPublicKey());
    }
}

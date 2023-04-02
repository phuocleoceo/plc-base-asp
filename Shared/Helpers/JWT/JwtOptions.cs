using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PlcBase.Shared.Helpers;

public static class JwtOptions
{
    public static TokenValidationParameters GetTokenParams(
        JwtSettings jwtSettings, SecurityKey securityKey)
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
            ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkew),
        };
    }

    public static SigningCredentials GetPrivateKey(JwtSettings jwtSettings)
    {
        string dirPath = AppDomain.CurrentDomain.BaseDirectory;
        string fileName = Path.Combine(dirPath, "Certificate", jwtSettings.PrivateKeyPath);
        string privateKeyPem = File.ReadAllText(fileName);

        privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
        privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

        byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

        RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
        provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
        RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);
        return new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
    }

    public static SecurityKey GetPublicKey(JwtSettings jwtSettings)
    {
        string dirPath = AppDomain.CurrentDomain.BaseDirectory;
        string fileName = Path.Combine(dirPath, "Certificate", jwtSettings.PublicKeyPath);
        X509Certificate2 cert = new X509Certificate2(fileName);
        RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(cert.GetRSAPublicKey());
        return rsaSecurityKey;
    }
}
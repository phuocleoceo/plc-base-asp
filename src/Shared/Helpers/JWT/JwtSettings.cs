namespace PlcBase.Shared.Helpers;

public class JwtSettings
{
    public long Expires { get; set; }
    public string PrivateKeyPath { get; set; }
    public string PublicKeyPath { get; set; }
    public string TokenType { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateIssuer { get; set; }
    public string ValidIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public string ValidAudience { get; set; }
    public int ClockSkew { get; set; }
    public long RefreshTokenExpires { get; set; }
}

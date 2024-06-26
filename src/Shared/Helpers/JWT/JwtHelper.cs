using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;

using PlcBase.Shared.Constants;
using PlcBase.Shared.Utilities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Error;

namespace PlcBase.Shared.Helpers;

public class JwtHelper : IJwtHelper
{
    private readonly JwtSettings _jwtSettings;

    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public TokenData CreateAccessToken(IEnumerable<Claim> claims)
    {
        List<Claim> claimsIdentity = claims.ToList();
        claimsIdentity.Add(new Claim(CustomClaimTypes.Type, TokenType.AccessToken));

        return WriteToken(BuildTokenDescriptor(claimsIdentity, _jwtSettings.Expires));
    }

    public TokenData CreateRefreshToken(IEnumerable<Claim> claims)
    {
        List<Claim> claimsIdentity = claims.ToList();
        claimsIdentity.Add(new Claim(CustomClaimTypes.Type, TokenType.RefreshToken));

        return WriteToken(BuildTokenDescriptor(claimsIdentity, _jwtSettings.RefreshTokenExpires));
    }

    private SecurityTokenDescriptor BuildTokenDescriptor(
        IEnumerable<Claim> claimsIdentity,
        long expireInSeconds
    )
    {
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsIdentity),
            Expires = TimeUtility.Now().AddSeconds(expireInSeconds),
            Audience = _jwtSettings.ValidateAudience ? _jwtSettings.ValidAudience : null,
            Issuer = _jwtSettings.ValidateIssuer ? _jwtSettings.ValidIssuer : null,
            SigningCredentials = new SigningCredentials(
                JwtOptions.GetPrivateKey(_jwtSettings.PrivateKeyPath),
                SecurityAlgorithms.RsaSha256
            )
        };
    }

    private TokenData WriteToken(SecurityTokenDescriptor tokenDescriptor)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenData()
        {
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = token.ValidTo,
        };
    }

    public TokenPrincipal ValidateAndGetTokenPrincipal(string token)
    {
        SecurityKey publicKey = JwtOptions.GetPublicKey(_jwtSettings.PublicKeyPath);

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = publicKey,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuer = false
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken
        );

        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

        return new TokenPrincipal()
        {
            ClaimsPrincipal = claimsPrincipal,
            JwtSecurityToken = jwtSecurityToken
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        TokenPrincipal tokenPrincipal = ValidateAndGetTokenPrincipal(token);
        JwtSecurityToken jwtSecurityToken = tokenPrincipal.JwtSecurityToken;
        ClaimsPrincipal claimsPrincipal = tokenPrincipal.ClaimsPrincipal;

        if (
            jwtSecurityToken == null
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.RsaSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
            throw new BaseException(HttpCode.BAD_REQUEST, ErrorMessage.INVALID_TOKEN);

        return claimsPrincipal;
    }
}

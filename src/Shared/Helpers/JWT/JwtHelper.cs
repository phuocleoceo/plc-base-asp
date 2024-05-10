using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;

using PlcBase.Shared.Constants;
using PlcBase.Shared.Utilities;
using PlcBase.Base.Error;

namespace PlcBase.Shared.Helpers;

public class JwtHelper : IJwtHelper
{
    private readonly JwtSettings _jwtSettings;

    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public TokenData CreateToken(IEnumerable<Claim> claims)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = TimeUtility.Now().AddSeconds(_jwtSettings.Expires),
            Audience = _jwtSettings.ValidateAudience ? _jwtSettings.ValidAudience : null,
            Issuer = _jwtSettings.ValidateIssuer ? _jwtSettings.ValidIssuer : null,
            SigningCredentials = new SigningCredentials(
                JwtOptions.GetPrivateKey(_jwtSettings.PrivateKeyPath),
                SecurityAlgorithms.RsaSha256
            )
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenData()
        {
            Token = tokenHandler.WriteToken(token),
            ExpiredAt = token.ValidTo,
        };
    }

    public TokenData CreateRefreshToken()
    {
        return new TokenData()
        {
            Token = CodeSecure.CreateRandomCode(32),
            ExpiredAt = TimeUtility.Now().AddSeconds(_jwtSettings.RefreshTokenExpires)
        };
    }

    public TokenPrincipal ValidateAndGetTokenPrincipal(string token)
    {
        SecurityKey publicKey = JwtOptions.GetPublicKey(_jwtSettings.PublicKeyPath);

        TokenValidationParameters tokenValidationParameters = JwtOptions.GetTokenParams(
            _jwtSettings,
            publicKey
        );

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

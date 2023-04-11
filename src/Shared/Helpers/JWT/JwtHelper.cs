using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;

using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Shared.Helpers;

public class JwtHelper : IJwtHelper
{
    private readonly JwtSettings _jwtSettings;

    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public TokenData CreateToken(List<Claim> claims)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Expires),
            SigningCredentials = JwtOptions.GetPrivateKey(_jwtSettings),
            Audience = _jwtSettings.ValidateAudience ? _jwtSettings.ValidAudience : null,
            Issuer = _jwtSettings.ValidateIssuer ? _jwtSettings.ValidIssuer : null,
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
            ExpiredAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpires)
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        SecurityKey publicKey = JwtOptions.GetPublicKey(_jwtSettings);

        TokenValidationParameters tokenValidationParameters = JwtOptions.GetTokenParams(
            _jwtSettings,
            publicKey
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken
        );

        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

        if (
            jwtSecurityToken == null
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.RsaSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
            throw new BaseException(HttpCode.BAD_REQUEST, ErrorMessage.INVALID_TOKEN);

        return principal;
    }
}

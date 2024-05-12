using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public interface IJwtHelper
{
    TokenData CreateAccessToken(IEnumerable<Claim> claims);

    TokenData CreateRefreshToken(IEnumerable<Claim> claims);

    TokenPrincipal ValidateAndGetTokenPrincipal(string token);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

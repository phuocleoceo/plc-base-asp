using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public interface IJwtHelper
{
    TokenData CreateToken(IEnumerable<Claim> claims);

    TokenData CreateRefreshToken();

    TokenPrincipal ValidateAndGetTokenPrincipal(string token);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

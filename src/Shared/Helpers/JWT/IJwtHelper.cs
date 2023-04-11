using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public interface IJwtHelper
{
    TokenData CreateToken(List<Claim> claims);

    TokenData CreateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

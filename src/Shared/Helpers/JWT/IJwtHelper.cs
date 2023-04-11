using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public interface IJwtHelper
{
    TokenData CreateToken(List<Claim> claims);

    string CreateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

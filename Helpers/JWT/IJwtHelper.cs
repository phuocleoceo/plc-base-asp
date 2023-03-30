using System.Security.Claims;

namespace PlcBase.Helpers;

public interface IJwtHelper
{
    TokenData CreateToken(List<Claim> claims);
}
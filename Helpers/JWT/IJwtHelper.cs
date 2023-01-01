using System.Security.Claims;

namespace PlcBase.Helpers;

public interface IJwtHelper
{
    string CreateToken(List<Claim> claims);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public class TokenPrincipal
{
    public ClaimsPrincipal ClaimsPrincipal { get; set; }

    public JwtSecurityToken JwtSecurityToken { get; set; }
}

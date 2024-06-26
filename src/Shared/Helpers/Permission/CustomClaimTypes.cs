using System.Security.Claims;

namespace PlcBase.Shared.Helpers;

public class CustomClaimTypes
{
    public const string UserId = "userId";

    public const string Email = "email";

    // public const string Permission = "permission";
    public const string Permission = ClaimTypes.Role;

    public const string Role = ClaimTypes.Role;
    public const string Type = "type";
}

using Microsoft.AspNetCore.Authorization;

namespace PlcBase.Base.Authorize;

public class RoleAuthorizeAttribute : AuthorizeAttribute
{
    public RoleAuthorizeAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}

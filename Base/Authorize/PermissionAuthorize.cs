using Microsoft.AspNetCore.Authorization;

namespace PlcBase.Base.Authorize;

public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    public PermissionAuthorizeAttribute(string Permission)
    {
        Roles = Permission;
    }
}
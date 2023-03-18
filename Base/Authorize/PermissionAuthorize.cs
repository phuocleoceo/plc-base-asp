using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PlcBase.Helpers;

namespace PlcBase.Base.Authorize;

public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly int _permission;

    public PermissionAuthorizeAttribute(int Permission)
    {
        _permission = Permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ClaimsPrincipal user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        IEnumerable<int> permissionClaims = user.FindAll(CustomClaimTypes.Permission)
                                                .Select(c => Convert.ToInt32(c.Value));

        if (!permissionClaims.Any(c => c == _permission))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
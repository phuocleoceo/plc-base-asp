using System.Security.Claims;

using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;

namespace PlcBase.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtHelper _jwtHelper;

    public JwtMiddleware(RequestDelegate next, IJwtHelper jwtHelper)
    {
        _next = next;
        _jwtHelper = jwtHelper;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            GetDataFromTokenPayload(context, token);

        await _next(context);
    }

    private void GetDataFromTokenPayload(HttpContext context, string token)
    {
        try
        {
            TokenPrincipal tokenPrincipal = _jwtHelper.ValidateAndGetTokenPrincipal(token);
            List<Claim> claims = tokenPrincipal.JwtSecurityToken.Claims.ToList();

            context.Items["reqUser"] = new ReqUser()
            {
                Id = Convert.ToInt32(claims.First(x => x.Type == CustomClaimTypes.UserId).Value),
                Email = claims.First(x => x.Type == CustomClaimTypes.Email).Value,
            };
        }
        catch
        {
            // throw new BaseException(HttpCode.BAD_REQUEST, ErrorMessage.INVALID_TOKEN);
        }
    }
}

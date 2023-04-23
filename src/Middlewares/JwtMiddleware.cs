using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;

namespace PlcBase.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSettings _jwtSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
    {
        _next = next;
        _jwtSettings = jwtSettings.Value;
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
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityKey publicKey = JwtOptions.GetPublicKey(_jwtSettings);

            TokenValidationParameters tokenValidationParameters = JwtOptions.GetTokenParams(
                _jwtSettings,
                publicKey
            );

            tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken validatedToken
            );

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

            // Get data from payload
            context.Items["reqUser"] = new ReqUser()
            {
                Id = Convert.ToInt32(
                    jwtToken.Claims.First(x => x.Type == CustomClaimTypes.UserId).Value
                ),
                Email = jwtToken.Claims.First(x => x.Type == CustomClaimTypes.Email).Value,
            };
        }
        catch
        {
            throw new BaseException(HttpCode.BAD_REQUEST, ErrorMessage.INVALID_TOKEN);
        }
    }
}

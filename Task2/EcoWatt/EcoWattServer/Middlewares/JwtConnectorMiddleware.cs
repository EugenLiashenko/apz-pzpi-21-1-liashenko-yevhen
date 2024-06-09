using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Jwt;
using Core.Entities;
using Core.Enums;
using Microsoft.IdentityModel.Tokens;

namespace EcoWattServer.Migrations;

public class JwtConnectorMiddleware
{
    private readonly RequestDelegate _request;

    public JwtConnectorMiddleware(RequestDelegate request)
    {
        _request = request;
    }
    
    public async Task Invoke(HttpContext context, IJwtTokenService jwtTokenService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null)
        {
            AddUserToContext(context, jwtTokenService, token);
        }

        await _request(context);
    }
    
    private void AddUserToContext(HttpContext context, IJwtTokenService jwtService, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtService.Configuration["Jwt:SecretKey"]);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        var userId = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        var role = Enum.Parse<RoleEnum>(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);

        context.Items["User"] = new User { Id = userId, Role = role };
    }
}
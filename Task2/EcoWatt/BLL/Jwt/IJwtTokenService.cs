using Core.Enums;
using Microsoft.Extensions.Configuration;

namespace BLL.Jwt;

public interface IJwtTokenService
{
    public IConfiguration Configuration { get; set; }

    string GenerateToken(int userId, RoleEnum role);
}
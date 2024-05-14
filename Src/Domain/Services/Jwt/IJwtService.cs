using System.Security.Claims;
using Domain.Services.Jwt.Types;

namespace Domain.Services.Jwt;

public interface IJwtService
{
    AccessToken GenerateAccessToken(IEnumerable<Claim> claims);

    RefreshToken GenerateRefreshToken();
}

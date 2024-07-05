using System.Security.Claims;
using Domain.Account.Models;
using Domain.Services.Jwt.Types;

namespace Domain.Services.Jwt;

public interface IJwtService
{
    AccessToken GenerateAccessToken(AccountModel account);

    RefreshToken GenerateRefreshToken();

    Task UpdateRefreshTokenAsync(
        string email,
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default
    );
}

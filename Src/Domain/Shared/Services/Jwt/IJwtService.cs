using Domain.Account.Models;
using Domain.Shared.Services.Jwt.Types;

namespace Domain.Shared.Services.Jwt;

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

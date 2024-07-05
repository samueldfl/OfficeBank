using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Account.Models;
using Domain.Shared.Services.Jwt;
using Domain.Shared.Services.Jwt.Claims;
using Domain.Shared.Services.Jwt.Types;
using Infra.Shared.Services.Jwt.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Shared.Services.Jwt;

internal class JwtService(JwtSettings settings, IDistributedCache distributedCache)
    : IJwtService
{
    private readonly IDistributedCache _distributedCache = distributedCache;
    private readonly JwtSettings _settings = settings;

    public AccessToken GenerateAccessToken(AccountModel account)
    {
        List<Claim> authClaims =
        [
            new Claim(JwtClaimsName.Id, account.Id.ToString()),
            new Claim(JwtClaimsName.Name, account.Name),
            new Claim(JwtClaimsName.Email, account.Email),
            new Claim(JwtClaimsName.BirthDate, account.BirthDate.ToString())
        ];

        SymmetricSecurityKey securityKey =
            new(Encoding.UTF8.GetBytes(_settings.SecurityKey));

        SigningCredentials signingCredentials =
            new(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token =
            new(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: signingCredentials
            );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AccessToken(tokenString);
    }

    public RefreshToken GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return new RefreshToken(Convert.ToBase64String(randomNumber));
    }

    public ClaimsPrincipal GetClaimsFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.SecurityKey)
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out var securityToken
        );

        if (
            securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
            throw new SecurityTokenException("Invalid Token");

        return principal;
    }

    public async Task<string> GetRefreshTokenAsync(
        string email,
        CancellationToken cancellationToken = default
    )
    {
        string key = $"account-{email}";
        string? cachedToken =
            await _distributedCache.GetStringAsync(key, cancellationToken)
            ?? throw new Exception("token nao encontrado");
        return cachedToken;
    }

    public async Task UpdateRefreshTokenAsync(
        string email,
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default
    )
    {
        string key = $"account-{email}";

        await _distributedCache.SetStringAsync(
            key,
            refreshToken.Value,
            cancellationToken
        );
    }
}

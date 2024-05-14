using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Services.Jwt;
using Domain.Services.Jwt.Settings;
using Domain.Services.Jwt.Types;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Services.Jwt;

internal class JwtService(JwtSettings settings) : IJwtService
{
    private readonly JwtSettings _settings = settings;

    public AccessToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        IEnumerable<Claim> authClaims = claims;
        SymmetricSecurityKey securityKey =
            new(Encoding.UTF8.GetBytes(_settings.SecurityKey));

        JwtSecurityToken token =
            new(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

        return new AccessToken(token.ToString());
    }

    public RefreshToken GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return new RefreshToken(Convert.ToBase64String(randomNumber));
    }
}

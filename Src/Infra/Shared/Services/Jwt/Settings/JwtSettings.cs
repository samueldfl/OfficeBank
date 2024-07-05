namespace Infra.Shared.Services.Jwt.Settings;

public sealed record JwtSettings(string Issuer, string Audience, string SecurityKey)
{
    public const string Section = "Jwt";
}

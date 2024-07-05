namespace Domain.Shared.Services.Jwt.Types;

public sealed record Token(AccessToken AccessToken, RefreshToken RefreshToken);

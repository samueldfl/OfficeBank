namespace Domain.Services.Jwt.Types;

public sealed record Token(AccessToken AccessToken, RefreshToken RefreshToken);

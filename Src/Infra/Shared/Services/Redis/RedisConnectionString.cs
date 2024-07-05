namespace Infra.Shared.Services.Redis;

internal sealed record RedisConnectionString(string Instance, string Password)
{
    public const string Section = "Redis";

    public override string ToString() => $"{Instance},password={Password}";
}

namespace Infra.Shared.Database.SqlServer.Config;

public class SqlServerConnect
{
    public string Server { get; init; } = string.Empty;

    public string UserId { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Encrypt { get; init; } = string.Empty;

    public string Database { get; init; } = string.Empty;

    public string TrustServerCertificate { get; init; } = string.Empty;

    public string GetConnectionString()
    {
        return $"Server={Server};User Id={UserId};Password={Password};Encrypt={Encrypt};Database={Database};TrustServerCertificate={TrustServerCertificate}";
    }
}

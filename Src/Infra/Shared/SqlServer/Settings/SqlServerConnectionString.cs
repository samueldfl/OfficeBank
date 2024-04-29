namespace Infra.Shared.SqlServer.Settings;

internal sealed record SqlServerConnectionString(
    string Server,
    string UserId,
    string Password,
    string Encrypt,
    string Database,
    string TrustServerCertificate
)
{
    public const string SqlServer = "SqlServer";

    public override string ToString()
    {
        return $"Server={Server};User Id={UserId};Password={Password};Encrypt={Encrypt};Database={Database};TrustServerCertificate={TrustServerCertificate}";
    }
}

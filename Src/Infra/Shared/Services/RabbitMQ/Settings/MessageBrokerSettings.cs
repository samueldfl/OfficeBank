namespace Infra.Shared.Services.RabbitMQ.Settings;

internal sealed record MessageBrokerSettings(
    string Host,
    string Username,
    string Password
)
{
    public const string Section = "RabbitMQ";
}

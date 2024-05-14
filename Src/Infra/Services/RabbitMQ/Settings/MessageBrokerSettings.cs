namespace Infra.Shared.Messengers.RabbitMQ.Config;

internal sealed record MessageBrokerSettings(
    string Host,
    string Username,
    string Password
)
{
    public const string Section = "RabbitMQ";
}

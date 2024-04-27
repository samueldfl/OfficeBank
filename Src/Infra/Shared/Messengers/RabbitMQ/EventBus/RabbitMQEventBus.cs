using Infra.Shared.Messengers.EventBus;
using MassTransit;

namespace Infra.Shared.Messengers.RabbitMQ.EventBus;

internal sealed class RabbitMQEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMQEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class => _publishEndpoint.Publish(message, cancellationToken);
}

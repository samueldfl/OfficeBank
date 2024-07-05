using Domain.Shared.Services.EventBus;
using MassTransit;

namespace Infra.Shared.Services.RabbitMQ.EventBus;

internal sealed class RabbitMQEventBus(IPublishEndpoint publishEndpoint)
    : IEventBusService
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class => _publishEndpoint.Publish(message, cancellationToken);
}

namespace Domain.Shared.Services.EventBus;

public interface IEventBusService
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;
}

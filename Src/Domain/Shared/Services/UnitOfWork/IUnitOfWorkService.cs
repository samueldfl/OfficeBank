namespace Domain.Shared.Services.UnitOfWork;

public interface IUnitOfWorkService
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}

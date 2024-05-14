namespace Domain.Services.UnitOfWork;

public interface IUnitOfWorkService
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}

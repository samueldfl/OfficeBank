namespace Infra.Shared.Database.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}

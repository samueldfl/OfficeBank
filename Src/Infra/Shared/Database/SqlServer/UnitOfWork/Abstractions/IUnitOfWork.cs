namespace Infra.Shared.Database.SqlServer.UnitOfWork.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
}


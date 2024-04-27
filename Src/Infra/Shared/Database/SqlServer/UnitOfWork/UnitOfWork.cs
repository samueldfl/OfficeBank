using Infra.Shared.Database.SqlServer.Context;
using Infra.Shared.Database.UnitOfWork;

namespace Infra.Shared.Database.SqlServer.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly SqlServerWriteContext _sqlContext;

    public UnitOfWork(SqlServerWriteContext sqlContext)
    {
        _sqlContext = sqlContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _sqlContext.SaveChangesAsync(cancellationToken);
    }
}

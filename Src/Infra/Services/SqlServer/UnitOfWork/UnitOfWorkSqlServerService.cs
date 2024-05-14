using Domain.Services.UnitOfWork;
using Infra.Shared.SqlServer.Context;

namespace Infra.Shared.SqlServer.UnitOfWork;

internal class UnitOfWorkSqlServerService : IUnitOfWorkService
{
    private readonly SqlServerWriteContext _sqlContext;

    public UnitOfWorkSqlServerService(SqlServerWriteContext sqlContext)
    {
        _sqlContext = sqlContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _sqlContext.SaveChangesAsync(cancellationToken);
    }
}

using Domain.Shared.Services.UnitOfWork;
using Infra.Shared.Services.SqlServer.Contexts;

namespace Infra.Shared.Services.SqlServer.UnitOfWork;

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

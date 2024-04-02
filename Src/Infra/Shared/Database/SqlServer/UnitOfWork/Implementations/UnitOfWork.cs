using Infra.Shared.Database.SqlServer.Context;
using Infra.Shared.Database.SqlServer.UnitOfWork.Abstractions;

namespace Infra.Shared.Database.SqlServer.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlServerWriteContext _sqlContext;

    public UnitOfWork(SqlServerWriteContext sqlContext)
    {
        _sqlContext = sqlContext;
    }

    public async Task CommitAsync()
    {
        await _sqlContext.SaveChangesAsync();
    }
}


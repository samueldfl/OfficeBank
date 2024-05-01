using Domain.Transfer.Models;
using Domain.Transfer.Repositories;
using Infra.Shared.SqlServer.Context;

namespace Infra.Transfer.Repositories;

internal class TransferRepository(SqlServerWriteContext sqlServerWriteContext)
    : ITransferRepository
{
    private readonly SqlServerWriteContext _sqlServerWriteContext = sqlServerWriteContext;

    public void Create(TransferModel model)
    {
        _sqlServerWriteContext.Transfers.Add(model);
    }
}

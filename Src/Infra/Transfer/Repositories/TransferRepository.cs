using Domain.Transfer.Models;
using Domain.Transfer.Repositories;
using Infra.Shared.Database.SqlServer.Context;

namespace Infra.Transfer.Repositories;

internal class TransferRepository : ITransferRepository
{
    private readonly SqlServerWriteContext _sqlWriteContext;

    public TransferRepository(SqlServerWriteContext sqlWriteContext)
    {
        _sqlWriteContext = sqlWriteContext;
    }

    public async Task CreateAsync(
        TransferModel model,
        CancellationToken cancellationToken = default
    )
    {
        await _sqlWriteContext.Transfers.AddAsync(model, cancellationToken);
    }
}

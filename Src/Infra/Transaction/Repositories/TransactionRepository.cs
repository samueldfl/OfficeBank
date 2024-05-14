using System.Linq.Expressions;
using Domain.Transaction.Models;
using Domain.Transaction.Repositories;
using Infra.Shared.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Transaction.Repositories;

internal sealed class TransactionRepository(
    SqlServerReadContext sqlServerReadContext,
    SqlServerWriteContext sqlServerWriteContext
) : ITransactionRepository
{
    private readonly SqlServerReadContext _sqlServerReadContext = sqlServerReadContext;
    private readonly SqlServerWriteContext _sqlServerWriteContext = sqlServerWriteContext;

    public void Create(TransactionModel model)
    {
        _sqlServerWriteContext.Transactions.Add(model);
    }

    public async Task<TransactionModel> ReadLastAsNoTrackingAsync(
        Expression<Func<TransactionModel, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        TransactionModel transaction =
            await _sqlServerReadContext
                .Transactions.AsQueryable()
                .OrderByDescending(transaction => transaction.CreatedAt)
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken)
            ?? throw new Exception();

        return transaction;
    }
}

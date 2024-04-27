using System.Linq.Expressions;
using Domain.Payment.Models;
using Domain.Transaction.Repositories;
using Infra.Shared.Database.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Transaction.Repositories;

internal sealed class TransactionRepository : ITransactionRepository
{
    private readonly SqlServerReadContext _sqlServerReadContext;
    private readonly SqlServerWriteContext _sqlServerWriteContext;

    public TransactionRepository(
        SqlServerReadContext sqlServerReadContext,
        SqlServerWriteContext sqlServerWriteContext
    )
    {
        _sqlServerReadContext = sqlServerReadContext;
        _sqlServerWriteContext = sqlServerWriteContext;
    }

    public async Task CreateTransactionAsync(
        TransactionModel model,
        CancellationToken cancellationToken = default
    )
    {
        await _sqlServerWriteContext.Transactions.AddAsync(model, cancellationToken);
    }

    public async Task<TransactionModel> GetLastAccountTransactionAsync(
        Expression<Func<TransactionModel, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        TransactionModel transaction =
            await _sqlServerWriteContext
                .Transactions.AsQueryable()
                .OrderByDescending(transaction => transaction.CreatedAt)
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken)
            ?? throw new Exception();

        return transaction;
    }

    public async Task<TransactionModel> GetLastAccountTransactionAsNoTrackingAsync(
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

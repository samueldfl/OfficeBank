using System.Linq.Expressions;
using Domain.Payment.Models;

namespace Domain.Transaction.Repositories;

public interface ITransactionRepository
{
    Task CreateTransactionAsync(
        TransactionModel transactionModel,
        CancellationToken cancellationToken = default
    );

    Task<TransactionModel> GetLastAccountTransactionAsync(
        Expression<Func<TransactionModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task<TransactionModel> GetLastAccountTransactionAsNoTrackingAsync(
        Expression<Func<TransactionModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

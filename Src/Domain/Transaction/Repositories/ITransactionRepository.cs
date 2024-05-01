using System.Linq.Expressions;
using Domain.Payment.Models;

namespace Domain.Transaction.Repositories;

public interface ITransactionRepository
{
    void Create(TransactionModel transactionModel);

    Task<TransactionModel> ReadLastAsNoTrackingAsync(
        Expression<Func<TransactionModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

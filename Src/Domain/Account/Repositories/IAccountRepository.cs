using System.Linq.Expressions;
using Domain.Account.Models;

namespace Domain.Account.Repositories;

public interface IAccountRepository
{
    Task<AccountModel> GetAccountAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task<AccountModel> GetAccountAsNoTrackingAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

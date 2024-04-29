using System.Linq.Expressions;
using Domain.Account.Models;

namespace Domain.Account.Repositories;

public interface IAccountRepository
{
    Task<AccountModel> ReadAccountAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task<AccountModel> ReadAccountAsNoTrackingAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

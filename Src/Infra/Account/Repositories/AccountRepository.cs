using System.Linq.Expressions;
using Domain.Account.Exceptions;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Infra.Shared.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Account.Repositories;

internal sealed class AccountRepository(
    SqlServerReadContext sqlServerReadContext,
    SqlServerWriteContext sqlServerWriteContext
) : IAccountRepository
{
    private readonly SqlServerReadContext _sqlServerReadContext = sqlServerReadContext;

    private readonly SqlServerWriteContext _sqlServerWriteContext = sqlServerWriteContext;

    public void Create(AccountModel accountModel)
    {
        _sqlServerWriteContext.Accounts.Add(accountModel);
    }

    public async Task<AccountModel> ReadAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        AccountModel account =
            await _sqlServerWriteContext.Accounts.FirstOrDefaultAsync(
                predicate,
                cancellationToken
            ) ?? throw new AccountNotFoundException();

        return account;
    }

    public async Task<AccountModel> ReadAsNoTrackingAsync(
        Expression<Func<AccountModel, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        AccountModel account =
            await _sqlServerReadContext.Accounts.FirstOrDefaultAsync(
                predicate,
                cancellationToken
            ) ?? throw new AccountNotFoundException();

        return account;
    }
}

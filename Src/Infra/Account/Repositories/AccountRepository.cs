using System.Linq.Expressions;
using Domain.Account.Exceptions;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Infra.Shared.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Account.Repositories;

internal sealed class AccountRepository : IAccountRepository
{
    private readonly SqlServerReadContext _sqlServerReadContext;

    private readonly SqlServerWriteContext _sqlServerWriteContext;

    public AccountRepository(
        SqlServerReadContext sqlServerReadContext,
        SqlServerWriteContext sqlServerWriteContext
    )
    {
        _sqlServerReadContext = sqlServerReadContext;
        _sqlServerWriteContext = sqlServerWriteContext;
    }

    public async Task<AccountModel> ReadAccountAsync(
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

    public async Task<AccountModel> ReadAccountAsNoTrackingAsync(
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

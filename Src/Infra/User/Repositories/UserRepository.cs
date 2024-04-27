using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.User.Models;
using Domain.User.Repositories;
using Infra.Shared.Database.SqlServer.Context;

namespace Infra.User.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly SqlServerReadContext _readSqlServerContext;

    private readonly SqlServerWriteContext _writeSqlServerContext;

    public UserRepository(
        SqlServerReadContext readSqlServerContext,
        SqlServerWriteContext writeSqlServerContext
    )
    {
        _readSqlServerContext = readSqlServerContext;
        _writeSqlServerContext = writeSqlServerContext;
    }

    public async Task<UserModel?> ReadUserAsync(
        Expression<Func<UserModel, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            UserModel? user = await _readSqlServerContext.Users.FirstOrDefaultAsync(
                predicate,
                cancellationToken
            );
            return user;
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

    public async Task CreateAsync(
        UserModel model,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await _writeSqlServerContext.Users.AddAsync(model, cancellationToken);
        }
        catch (Exception) { }
    }
}

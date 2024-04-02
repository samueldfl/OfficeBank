using System.Linq.Expressions;
using Domain.User.Models;

namespace Domain.User.Repositories;

public interface IUserRepository
{
    Task<UserModel?> ReadUserAsync(
        Expression<Func<UserModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    Task CreateAsync(UserModel model, CancellationToken cancellationToken = default);
}

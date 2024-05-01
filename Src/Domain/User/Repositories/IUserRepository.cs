using System.Linq.Expressions;
using Domain.User.Models;

namespace Domain.User.Repositories;

public interface IUserRepository
{
    void Create(UserModel model);

    Task<UserModel?> ReadUserAsync(
        Expression<Func<UserModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

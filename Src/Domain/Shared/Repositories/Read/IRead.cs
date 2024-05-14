using System.Linq.Expressions;
using Domain.Shared.Models;
using Domain.Shared.Repositories;

namespace Domain.Shared.Repositories.Read;

public interface IRead<TModel> : IRepository
    where TModel : BaseModel
{
    Task<TModel> ReadAsync(
        Expression<Func<TModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

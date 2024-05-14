using System.Linq.Expressions;
using Domain.Shared.Models;

namespace Domain.Shared.Repositories.Read;

public interface IReadAsNoTracking<TModel>
    where TModel : BaseModel
{
    Task<TModel> ReadAsNoTrackingAsync(
        Expression<Func<TModel, bool>> predicate,
        CancellationToken cancellationToken = default
    );
}

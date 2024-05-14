using Domain.Shared.Models;
using Domain.Shared.Repositories;

namespace Domain.Shared.Repositories.Create;

public interface ICreateRepository<TModel> : IRepository
    where TModel : BaseModel
{
    void Create(TModel model);
}

using Domain.Shared.Command;

namespace Application.Shared.Mappers;
public interface IMapper<in TCommand, TModel> where TCommand : ICommand
{
    TModel CommandToModel(TCommand command);
}

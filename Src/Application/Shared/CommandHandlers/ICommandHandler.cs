using Application.Shared.ResultStates;
using Domain.Shared.Commands;

namespace Application.Shared.CommandHandlers;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<RootResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

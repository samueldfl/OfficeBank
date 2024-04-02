using Application.Shared.Identities;

using Domain.Shared.Command;

namespace Application.Shared.CommandHandlers;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

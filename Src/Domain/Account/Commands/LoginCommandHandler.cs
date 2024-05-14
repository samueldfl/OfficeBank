using Domain.Shared.Commands;
using Domain.Shared.ValidationStates;

namespace Domain.Account.Commands;

public sealed record LoginCommandHandler : ICommand
{
    public ValidationState Validate()
    {
        throw new NotImplementedException();
    }
}

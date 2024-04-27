using Domain.Shared.Commands;
using Domain.Shared.ValidationStates;

namespace Application.Transaction.Commands;

public sealed record DepositCommand(string ToAccountId, decimal Amount) : ICommand
{
    public ValidationState Validate()
    {
        throw new NotImplementedException();
    }
}

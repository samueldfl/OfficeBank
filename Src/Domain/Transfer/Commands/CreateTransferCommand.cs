using Domain.Shared.Commands;
using Domain.Shared.ValidationStates;

namespace Domain.Transfer.Commands;

public record CreateTransferCommand(
    string FromAccountId,
    string ToAccountId,
    decimal Amount
) : ICommand
{
    public ValidationState Validate()
    {
        return new FailureValidationState();
    }
}

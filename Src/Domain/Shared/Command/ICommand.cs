using Domain.Shared.ValidationStates;

namespace Domain.Shared.Commands;

public interface ICommand
{
    ValidationState Validate();
}

using Domain.Shared.ValidationStates;

namespace Domain.Shared.Query;

public interface IQuery
{
    ValidationState Validate();
}

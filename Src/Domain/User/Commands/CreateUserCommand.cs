using Domain.Shared.Commands;
using Domain.Shared.ValidationStates;
using Domain.User.Records;
using Domain.User.Validators;

namespace Domain.User.Commands;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    string CPF,
    UserAddress Address
) : ICommand
{
    public ValidationState Validate()
    {
        CreateUserCommandValidator validator = new();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            Dictionary<string, string> errors = [];
            foreach (var err in result.Errors)
            {
                errors[err.PropertyName] = err.ErrorMessage;
            }
            return new FailureValidationState() { Body = errors };
        }

        return new SucessValidationState();
    }
};

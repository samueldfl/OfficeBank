using Domain.Account.Records;
using Domain.Account.Validators;
using Domain.Shared.Commands;
using Domain.Shared.ValidationStates;

namespace Domain.Account.Commands;

public record RegisterAccountCommand(
    string Name,
    string Email,
    string Password,
    string CPF,
    AddressCommand Address
) : ICommand
{
    public ValidationState Validate()
    {
        CreateAccountCommandValidator validator = new();
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

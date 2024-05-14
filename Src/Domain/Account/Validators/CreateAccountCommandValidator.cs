using Domain.Account.Commands;
using FluentValidation;

namespace Domain.Account.Validators;

public sealed class CreateAccountCommandValidator
    : AbstractValidator<RegisterAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(5).MaximumLength(60);

        RuleFor(p => p.Email).EmailAddress();
        RuleFor(p => p.Password).Must(ValidatePassword);
        RuleFor(p => p.CPF).Must(ValidateCPF);
        RuleFor(p => p.Address)
            .ChildRules(address =>
            {
                address.RuleFor(p => p.Street).NotEmpty();
                address.RuleFor(p => p.Number).NotEmpty();
                address.RuleFor(p => p.ZipCode).NotEmpty();
                address.RuleFor(p => p.Neighborhood).NotEmpty();
                address.RuleFor(p => p.City).NotEmpty();
                address.RuleFor(p => p.State).NotEmpty();
                address.RuleFor(p => p.Country).NotEmpty();
            });
    }

    private static bool ValidatePassword(string Password)
    {
        return true;
    }

    private static bool ValidateCPF(string Password)
    {
        return true;
    }
}

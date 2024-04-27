using Domain.Transfer.Models;

using FluentValidation;

namespace Domain.Transfer.Validators;

public class CreateTransferCommandValidator : AbstractValidator<TransferModel>
{
    public CreateTransferCommandValidator() { }
}

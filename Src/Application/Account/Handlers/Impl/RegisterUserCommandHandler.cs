using Application.Account.Handlers.Abst;
using Application.Shared.ResultStates;
using Domain.Account.Commands;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Domain.Services.Encrypter;
using Domain.Services.UnitOfWork;
using Domain.Shared.ValidationStates;

namespace Application.Account.Handlers.Impl;

public sealed class RegisterAccountCommandHandler(
    IAccountRepository accountRepository,
    IEncrypterService encrypterService,
    IUnitOfWorkService unitOfWorkService
) : IRegisterAccountCommandHandler
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    private readonly IEncrypterService _encrypterService = encrypterService;

    private readonly IUnitOfWorkService _unitOfWorkService = unitOfWorkService;

    public async Task<RootResult> HandleAsync(
        RegisterAccountCommand command,
        CancellationToken cancellationToken
    )
    {
        ValidationState validation = command.Validate();

        if (validation is FailureValidationState)
            return new RootBadRequestResult() { Body = validation.Body };

        AccountModel account = command;

        try
        {
            account.Password = _encrypterService.Hash(account.Password);
            account.CPF = _encrypterService.Hash(account.CPF);
            _accountRepository.Create(account);
            await _unitOfWorkService.CommitAsync(cancellationToken);
            return new RootCreatedResult();
        }
        catch (Exception e)
        {
            return new RootFailureResult() { Body = e.Message };
        }
    }
}

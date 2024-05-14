using Application.Account.Handlers.Abst;
using Application.Shared.ResultStates;
using Domain.Account.Commands;
using Domain.Account.Repositories;
using Domain.Services.Jwt;
using Domain.Shared.ValidationStates;

namespace Application.Account.Handlers.Impl;

internal class LoginAccountCommandHandler(
    IAccountRepository accountRepository,
    IJwtService jwtService
) : ILoginAccountCommandHandler
{
    public IAccountRepository _accountRepository = accountRepository;

    public IJwtService _jwtService = jwtService;

    public async Task<RootResult> HandleAsync(
        LoginCommandHandler command,
        CancellationToken cancellationToken
    )
    {
        ValidationState validation = command.Validate();

        if (validation is FailureValidationState)
            return new RootBadRequestResult() { Body = validation.Body };

        try 
        {
            await _accountRepository.ReadAsNoTrackingAsync(account => account.Id);
        }
        catch (Exception e) { }

        throw new NotImplementedException();
    }
}

using Application.Shared.CommandHandlers;
using Application.Shared.ResultStates;
using Application.Transaction.Commands;
using Application.Transaction.Handlers.Abst;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Domain.Payment.Models;
using Domain.Shared.Services.UnitOfWork;
using Domain.Shared.ValidationStates;
using Domain.Transaction.Repositories;

namespace Application.Transaction.Handlers.Impl;

internal sealed class DepositCommandHandler(
    IAccountRepository accountRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWorkService unitOfWork
) : IDepositCommandHandler
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    private readonly ITransactionRepository _transactionRepository =
        transactionRepository;

    private readonly IUnitOfWorkService _unitOfWork = unitOfWork;

    public async Task<RootResult> HandleAsync(
        DepositCommand command,
        CancellationToken cancellationToken
    )
    {
        ValidationState validation = command.Validate();

        if (validation is FailureValidationState)
        {
            return new RootBadRequestResult();
        }

        try
        {
            AccountModel toAccount =
                await _accountRepository.ReadAsNoTrackingAsync(
                    account => account.Id == Guid.Parse(command.ToAccountId),
                    cancellationToken
                );

            TransactionModel lastTransaction =
                await _transactionRepository.ReadLastAsNoTrackingAsync(
                    account => account.Id == Guid.Parse(command.ToAccountId),
                    cancellationToken
                );

            TransactionModel depositTransaction =
                new()
                {
                    LastBalance = lastTransaction.Balance,
                    Balance = lastTransaction.Balance + command.Amount,
                    BalanceDiff = command.Amount,
                    AccountId = toAccount.Id,
                };

            _transactionRepository.Create(depositTransaction);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new RootOkResult();
        }
        catch (Exception e)
        {
            return new RootFailureResult() { Body = e.Message };
        }
    }
}

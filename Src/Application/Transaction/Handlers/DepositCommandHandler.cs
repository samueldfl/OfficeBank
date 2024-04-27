using Application.Shared.CommandHandlers;
using Application.Shared.ResultStates;
using Application.Transaction.Commands;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Domain.Payment.Models;
using Domain.Shared.ValidationStates;
using Domain.Transaction.Repositories;
using Infra.Shared.Database.UnitOfWork;

namespace Application.Transaction.Handlers;

public sealed class DepositCommandHandler : ICommandHandler<DepositCommand>
{
    private readonly IAccountRepository _accountRepository;

    private readonly ITransactionRepository _transactionRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DepositCommandHandler(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

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
            AccountModel toAccount = await _accountRepository.GetAccountAsNoTrackingAsync(
                account => account.Id == Guid.Parse(command.ToAccountId),
                cancellationToken
            );

            TransactionModel lastTransaction =
                await _transactionRepository.GetLastAccountTransactionAsNoTrackingAsync(
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

            await _transactionRepository.CreateTransactionAsync(
                depositTransaction,
                cancellationToken
            );

            await _unitOfWork.CommitAsync(cancellationToken);

            return new RootOkResult();
        }
        catch (Exception e)
        {
            return new RootFailureResult() { Body = e.Message };
        }
    }
}

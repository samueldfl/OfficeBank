using Application.Shared.ResultStates;
using Application.Transfer.Handlers.Abst;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Domain.Payment.Models;
using Domain.Shared.ValidationStates;
using Domain.Transaction.Repositories;
using Domain.Transfer.Commands;
using Domain.Transfer.Models;
using Domain.Transfer.Repositories;
using Infra.Shared.Database.UnitOfWork;
using Infra.Shared.Messengers.EventBus;

namespace Application.Transfer.Handlers.Impl;

public class CreateTransferCommandHandler : ICreateTransferCommandHandler
{
    private readonly IAccountRepository _accountRepository;

    private readonly ITransferRepository _transferRepository;

    private readonly ITransactionRepository _transactionRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IEventBus _eventBus;

    public CreateTransferCommandHandler(
        IAccountRepository accountRepository,
        ITransferRepository transferRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus
    )
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _transferRepository = transferRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<RootResult> HandleAsync(
        CreateTransferCommand command,
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
            AccountModel fromAccount =
                await _accountRepository.GetAccountAsNoTrackingAsync(
                    account => account.Id == Guid.Parse(command.FromAccountId),
                    cancellationToken
                );

            TransactionModel fromAccountLastTransaction =
                await _transactionRepository.GetLastAccountTransactionAsNoTrackingAsync(
                    account => account.AccountId == fromAccount.Id,
                    cancellationToken
                );

            if (
                fromAccountLastTransaction.Balance <= 0
                || command.Amount > fromAccountLastTransaction.Balance
            )
            {
                return new RootUnauthorizedResult();
            }

            AccountModel toAccount = await _accountRepository.GetAccountAsNoTrackingAsync(
                account => account.Id == Guid.Parse(command.ToAccountId),
                cancellationToken
            );

            TransactionModel toAccountLastTransaction =
                await _transactionRepository.GetLastAccountTransactionAsNoTrackingAsync(
                    transaction => transaction.AccountId == toAccount.Id,
                    cancellationToken
                );

            TransactionModel newFromAccountTransaction =
                new()
                {
                    Balance = fromAccountLastTransaction.Balance - command.Amount,
                    LastBalance = fromAccountLastTransaction.Balance,
                    BalanceDiff = -command.Amount,
                    AccountId = fromAccountLastTransaction.AccountId
                };

            await _transactionRepository.CreateTransactionAsync(
                newFromAccountTransaction,
                cancellationToken
            );

            TransactionModel newToAccountTransaction =
                new()
                {
                    Balance = toAccountLastTransaction.Balance + command.Amount,
                    LastBalance = toAccountLastTransaction.Balance,
                    BalanceDiff = command.Amount,
                    AccountId = toAccountLastTransaction.AccountId
                };

            await _transactionRepository.CreateTransactionAsync(
                newToAccountTransaction,
                cancellationToken
            );

            TransferModel transfer =
                new()
                {
                    Amount = command.Amount,
                    FromAccountId = fromAccount.Id,
                    ToAccountId = toAccount.Id,
                };

            await _transferRepository.CreateAsync(transfer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventBus.PublishAsync(transfer, cancellationToken);

            return new RootCreatedResult();
        }
        catch (Exception e)
        {
            return new RootFailureResult() { Body = e.Message };
        }
    }
}

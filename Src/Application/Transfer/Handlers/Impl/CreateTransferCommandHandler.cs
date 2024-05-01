using Application.Shared.ResultStates;
using Application.Transfer.Handlers.Abst;
using Domain.Account.Models;
using Domain.Account.Repositories;
using Domain.Payment.Models;
using Domain.Shared.Services.EventBus;
using Domain.Shared.Services.UnitOfWork;
using Domain.Shared.ValidationStates;
using Domain.Transaction.Repositories;
using Domain.Transfer.Commands;
using Domain.Transfer.Models;
using Domain.Transfer.Repositories;

namespace Application.Transfer.Handlers.Impl;

public sealed class CreateTransferCommandHandler(
    IAccountRepository accountRepository,
    ITransferRepository transferRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWorkService unitOfWork,
    IEventBusService eventBus
) : ICreateTransferCommandHandler
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    private readonly ITransferRepository _transferRepository = transferRepository;

    private readonly ITransactionRepository _transactionRepository =
        transactionRepository;

    private readonly IUnitOfWorkService _unitOfWork = unitOfWork;

    private readonly IEventBusService _eventBus = eventBus;

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
            AccountModel fromAccount = await _accountRepository.ReadAsNoTrackingAsync(
                account => account.Id == Guid.Parse(command.FromAccountId),
                cancellationToken
            );

            TransactionModel fromAccountLastTransaction =
                await _transactionRepository.ReadLastAsNoTrackingAsync(
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

            AccountModel toAccount = await _accountRepository.ReadAsNoTrackingAsync(
                account => account.Id == Guid.Parse(command.ToAccountId),
                cancellationToken
            );

            TransactionModel toAccountLastTransaction =
                await _transactionRepository.ReadLastAsNoTrackingAsync(
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

            _transactionRepository.Create(newFromAccountTransaction);

            TransactionModel newToAccountTransaction =
                new()
                {
                    Balance = toAccountLastTransaction.Balance + command.Amount,
                    LastBalance = toAccountLastTransaction.Balance,
                    BalanceDiff = command.Amount,
                    AccountId = toAccountLastTransaction.AccountId
                };

            _transactionRepository.Create(newToAccountTransaction);

            TransferModel transfer =
                new()
                {
                    Amount = command.Amount,
                    FromAccountId = fromAccount.Id,
                    ToAccountId = toAccount.Id,
                };

            _transferRepository.Create(transfer);
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

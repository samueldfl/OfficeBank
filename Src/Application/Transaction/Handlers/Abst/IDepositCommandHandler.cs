using Application.Shared.CommandHandlers;
using Application.Transaction.Commands;

namespace Application.Transaction.Handlers.Abst;

public interface IDepositCommandHandler : ICommandHandler<DepositCommand> { }

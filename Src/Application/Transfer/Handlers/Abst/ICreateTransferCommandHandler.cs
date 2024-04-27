using Application.Shared.CommandHandlers;
using Domain.Transfer.Commands;

namespace Application.Transfer.Handlers.Abst;

public interface ICreateTransferCommandHandler
    : ICommandHandler<CreateTransferCommand> { }

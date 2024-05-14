using Application.Shared.CommandHandlers;
using Domain.Account.Commands;

namespace Application.Account.Handlers.Abst;

public interface ILoginAccountCommandHandler : ICommandHandler<LoginCommandHandler> { }

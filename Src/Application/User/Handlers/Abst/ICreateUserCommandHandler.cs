using Application.Shared.CommandHandlers;
using Domain.User.Commands;

namespace Application.User.Handlers.Abst;

public interface ICreateUserCommandHandler : ICommandHandler<CreateUserCommand> { }

using Application.Shared.CommandHandlers;
using Domain.User.Commands;

namespace Application.User.Auth.Handlers.Abstractions;

public interface ICreateUserCommandHandler : ICommandHandler<CreateUserCommand> { }

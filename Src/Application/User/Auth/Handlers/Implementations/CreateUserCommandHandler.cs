using Application.Shared.Identities;
using Application.User.Auth.Mappers.Abstractions;
using Application.User.Auth.Handlers.Abstractions;
using Domain.User.Models;
using Domain.Shared.Identities;
using Domain.User.Repositories;
using Infra.Shared.Database.SqlServer.UnitOfWork.Abstractions;
using Domain.User.Commands;

namespace Application.User.Auth.Handlers;

public class CreateUserCommandHandler : ICreateUserCommandHandler
{
    private readonly ICreateUserMapper _createUserMapper;

    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        ICreateUserMapper createUserMapper,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    )
    {
        _createUserMapper = createUserMapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        Notifier notifier = command.Validate();

        if (notifier.HasFailure)
            return new() { Code = RootStatusCode.ERROR, Body = notifier.Notifications };

        UserModel user = _createUserMapper.CommandToModel(command);

        try
        {
            await _userRepository.CreateAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync();
            return new() { Code = RootStatusCode.CREATED };
        }
        catch (Exception ex)
        {
            return new() { Code = RootStatusCode.ERROR, Body = ex.Message };
        }
    }
}

using Application.Shared.ResultStates;
using Application.User.Handlers.Abst;
using Domain.Shared.Encrypter;
using Domain.Shared.Services.UnitOfWork;
using Domain.Shared.ValidationStates;
using Domain.User.Commands;
using Domain.User.Models;
using Domain.User.Repositories;

namespace Application.User.Handlers.Impl;

public class CreateUserCommandHandler : ICreateUserCommandHandler
{
    private readonly IUserRepository _userRepository;

    private readonly IEncrypterService _encrypterService;

    private readonly IUnitOfWorkService _unitOfWorkService;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IEncrypterService encrypterService,
        IUnitOfWorkService unitOfWorkService
    )
    {
        _userRepository = userRepository;
        _encrypterService = encrypterService;
        _unitOfWorkService = unitOfWorkService;
    }

    public async Task<RootResult> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        ValidationState validation = command.Validate();

        if (validation is FailureValidationState)
            return new RootBadRequestResult() { Body = validation.Body };

        UserModel user = command;

        try
        {
            user.Password = _encrypterService.Hash(user.Password);
            user.CPF = _encrypterService.Hash(user.CPF);
            await _userRepository.CreateAsync(user, cancellationToken);
            await _unitOfWorkService.CommitAsync(cancellationToken);
            return new RootCreatedResult();
        }
        catch (Exception e)
        {
            return new RootFailureResult() { Body = e.Message };
        }
    }
}

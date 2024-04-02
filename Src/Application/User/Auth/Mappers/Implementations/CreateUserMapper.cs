using Application.User.Auth.Mappers.Abstractions;
using Domain.User.Commands;
using Domain.User.Models;
using Infra.Shared.Encrypter.Services.Abstractions;

namespace Application.User.Auth.Mappers.Implementations;

public class CreateUserMapper : ICreateUserMapper
{
    private readonly IEncrypterService _encrypterService;

    public CreateUserMapper(IEncrypterService encrypterService)
    {
        _encrypterService = encrypterService;
    }

    public UserModel CommandToModel(CreateUserCommand command)
    {
        UserModel user = command;

        user.CPF = _encrypterService.Hash(user.CPF);
        user.Password = _encrypterService.Hash(user.Password);

        return user;
    }
}

using Application.Shared.Mappers;
using Domain.User.Commands;
using Domain.User.Models;

namespace Application.User.Auth.Mappers.Abstractions;

public interface ICreateUserMapper : IMapper<CreateUserCommand, UserModel> { }

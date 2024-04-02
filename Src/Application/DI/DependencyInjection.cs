using Microsoft.Extensions.DependencyInjection;

using Application.User.Auth.Handlers;
using Application.User.Auth.Mappers.Abstractions;
using Application.User.Auth.Handlers.Abstractions;
using Application.User.Auth.Mappers.Implementations;
using Infra.Shared.Encrypter.Services.Abstractions;
using Infra.Shared.Encrypter.Services.Implementations;

namespace Application.DI;

public static class DependencyInjection
{
    public static void ApplicationPersist(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserMapper, CreateUserMapper>();
        services.AddScoped<IEncrypterService, EncrypterService>();
        services.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
    }
}

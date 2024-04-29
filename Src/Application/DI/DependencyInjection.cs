using Application.Transaction.Handlers.Abst;
using Application.Transaction.Handlers.Impl;
using Application.Transfer.Handlers.Abst;
using Application.Transfer.Handlers.Impl;
using Application.User.Handlers.Abst;
using Application.User.Handlers.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationPersist(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
        services.AddScoped<ICreateTransferCommandHandler, CreateTransferCommandHandler>();
        services.AddScoped<IDepositCommandHandler, DepositCommandHandler>();

        return services;
    }
}

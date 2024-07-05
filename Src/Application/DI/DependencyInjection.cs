﻿using Application.Account.Handlers.Abst;
using Application.Account.Handlers.Impl;
using Application.Transaction.Handlers.Abst;
using Application.Transaction.Handlers.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection ApplicationPersist(this IServiceCollection services)
    {
        services.AddScoped<
            IRegisterAccountCommandHandler,
            RegisterAccountCommandHandler
        >();
        services.AddScoped<IDepositCommandHandler, DepositCommandHandler>();
        services.AddScoped<
            IRegisterAccountCommandHandler,
            RegisterAccountCommandHandler
        >();
        services.AddScoped<ILoginAccountCommandHandler, LoginAccountCommandHandler>();

        return services;
    }
}

using Domain.Account.Repositories;
using Domain.Transaction.Repositories;
using Domain.Transfer.Repositories;
using Domain.User.Repositories;
using Infra.Account.Repositories;
using Infra.Shared.Database.SqlServer.Context;
using Infra.Shared.Database.SqlServer.Settings;
using Infra.Shared.Database.SqlServer.UnitOfWork;
using Infra.Shared.Database.UnitOfWork;
using Infra.Shared.Encrypter.Abst;
using Infra.Shared.Encrypter.Impl;
using Infra.Shared.Messengers.EventBus;
using Infra.Shared.Messengers.RabbitMQ.Config;
using Infra.Shared.Messengers.RabbitMQ.EventBus;
using Infra.Transaction.Repositories;
using Infra.Transfer.Repositories;
using Infra.User.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DI;

public static class DependencyInjection
{
    public static IServiceCollection InfraPersist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<SqlServerReadContext>(options =>
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        services.AddDbContext<SqlServerWriteContext>();

        services.AddSingleton(
            configuration
                .GetSection(SqlServerConnectionString.SqlServer)
                .Get<SqlServerConnectionString>()!
        );

        services.AddSingleton(
            configuration
                .GetSection(MessageBrokerSettings.RabbitMQ)
                .Get<MessageBrokerSettings>()!
        );

        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            busConfig.UsingRabbitMq(
                (context, config) =>
                {
                    MessageBrokerSettings broker =
                        context.GetRequiredService<MessageBrokerSettings>();

                    config.Host(
                        new Uri(broker.Host),
                        h =>
                        {
                            h.Username(broker.Username);
                            h.Password(broker.Password);
                        }
                    );
                }
            );
        });

        services.AddScoped<IEventBus, RabbitMQEventBus>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEncrypterService, EncrypterService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}

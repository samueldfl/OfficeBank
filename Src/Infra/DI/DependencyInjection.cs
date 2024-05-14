using Domain.Account.Repositories;
using Domain.Services.Encrypter;
using Domain.Services.EventBus;
using Domain.Services.Jwt;
using Domain.Services.Jwt.Settings;
using Domain.Services.UnitOfWork;
using Domain.Transaction.Repositories;
using Domain.Transfer.Repositories;
using Infra.Account.Repositories;
using Infra.Services.Jwt;
using Infra.Services.Messengers.RabbitMQ.EventBus;
using Infra.Shared.Encrypter;
using Infra.Shared.Messengers.RabbitMQ.Config;
using Infra.Shared.SqlServer.Context;
using Infra.Shared.SqlServer.Settings;
using Infra.Shared.SqlServer.UnitOfWork;
using Infra.Transaction.Repositories;
using Infra.Transfer.Repositories;
using MassTransit;
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
        services.AddDbContext<SqlServerReadContext>();
        services.AddDbContext<SqlServerWriteContext>();

        services.AddSingleton(
            configuration
                .GetSection(SqlServerConnectionString.Section)
                .Get<SqlServerConnectionString>()!
        );

        services.AddSingleton(
            configuration
                .GetSection(MessageBrokerSettings.Section)
                .Get<MessageBrokerSettings>()!
        );

        services.AddSingleton(
            configuration.GetSection(JwtSettings.Section).Get<JwtSettings>()!
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

        services.AddScoped<IEventBusService, RabbitMQEventBus>();
        services.AddScoped<IUnitOfWorkService, UnitOfWorkSqlServerService>();
        services.AddScoped<IEncrypterService, EncrypterService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}

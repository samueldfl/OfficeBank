using Domain.Account.Repositories;
using Domain.Services.Encrypter;
using Domain.Services.EventBus;
using Domain.Services.Jwt;
using Domain.Services.UnitOfWork;
using Domain.Transaction.Repositories;
using Infra.Account.Repositories;
using Infra.Services.Jwt;
using Infra.Services.Jwt.Settings;
using Infra.Services.Messengers.RabbitMQ.EventBus;
using Infra.Services.Redis;
using Infra.Services.SqlServer.Contexts;
using Infra.Services.SqlServer.Settings;
using Infra.Services.SqlServer.UnitOfWork;
using Infra.Shared.Encrypter;
using Infra.Shared.Messengers.RabbitMQ.Config;
using Infra.Transaction.Repositories;
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
        services.AddDbContext<SqlServerWriteContext>();
        services.AddDbContext<SqlServerReadContext>();

        services.AddStackExchangeRedisCache(options =>
        {
            var redisConnection = configuration
                .GetSection(RedisConnectionString.Section)
                .Get<RedisConnectionString>()!;

            options.Configuration = redisConnection.ToString();
        });

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
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}

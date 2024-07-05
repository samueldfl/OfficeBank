using Domain.Account.Repositories;
using Domain.Shared.Services.Encrypter;
using Domain.Shared.Services.EventBus;
using Domain.Shared.Services.Jwt;
using Domain.Shared.Services.UnitOfWork;
using Domain.Transaction.Repositories;
using Infra.Account.Repositories;
using Infra.Shared.Services.Encrypter;
using Infra.Shared.Services.Jwt;
using Infra.Shared.Services.Jwt.Settings;
using Infra.Shared.Services.RabbitMQ.EventBus;
using Infra.Shared.Services.RabbitMQ.Settings;
using Infra.Shared.Services.Redis;
using Infra.Shared.Services.SqlServer.Contexts;
using Infra.Shared.Services.SqlServer.Settings;
using Infra.Shared.Services.SqlServer.UnitOfWork;
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

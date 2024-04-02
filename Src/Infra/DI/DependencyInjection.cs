using Domain.User.Repositories;
using Infra.Shared.Database.SqlServer.Context;
using Infra.Shared.Database.SqlServer.UnitOfWork.Abstractions;
using Infra.Shared.Database.SqlServer.UnitOfWork.Implementations;
using Infra.Shared.Encrypter.Services.Abstractions;
using Infra.Shared.Encrypter.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infra.User.Repositories;

namespace Infra.DI;

public static class DependencyInjection
{
    public static void InfraPersist(this IServiceCollection services)
    {
        services.AddDbContext<SqlServerReadContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddDbContext<SqlServerWriteContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEncrypterService, EncrypterService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}

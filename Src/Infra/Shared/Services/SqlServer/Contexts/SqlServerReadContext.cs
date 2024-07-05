using Domain.Account.Models;
using Domain.Transaction.Models;
using Infra.Shared.Services.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.Services.SqlServer.Contexts;

internal sealed class SqlServerReadContext(
    DbContextOptions<SqlServerReadContext> options,
    SqlServerConnectionString sqlServerConnectionString
) : DbContext(options)
{
    private readonly SqlServerConnectionString _sqlServerConnectionString =
        sqlServerConnectionString;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SqlServerReadContext).Assembly,
            WriteConfigurationsFilter
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_sqlServerConnectionString.ToString())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("SqlMappers.Read") ?? false;

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }
}

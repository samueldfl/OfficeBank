using Domain.Account.Models;
using Domain.Transaction.Models;
using Domain.Transfer.Models;
using Infra.Shared.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.SqlServer.Context;

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
            typeof(SqlServerWriteContext).Assembly,
            WriteConfigurationsFilter
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        bool isTesting = false;

        if (isTesting)
        {
            optionsBuilder.UseInMemoryDatabase("");
        }
        else
        {
            optionsBuilder.UseSqlServer(_sqlServerConnectionString.ToString());
        }

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransferModel> Transfers { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }
}

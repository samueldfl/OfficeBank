using Domain.Account.Models;
using Domain.Transaction.Models;
using Infra.Shared.Services.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.Services.SqlServer.Contexts;

internal sealed class SqlServerWriteContext(
    DbContextOptions<SqlServerWriteContext> options,
    SqlServerConnectionString sqlServerConnection
) : DbContext(options)
{
    private readonly SqlServerConnectionString _sqlServerConnectionString =
        sqlServerConnection;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SqlServerWriteContext).Assembly,
            WriteConfigurationsFilter
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_sqlServerConnectionString.ToString());
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("SqlMappers.Write") ?? false;

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }
}

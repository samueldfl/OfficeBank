using Domain.Account.Models;
using Domain.Payment.Models;
using Domain.Transfer.Models;
using Domain.User.Models;
using Infra.Shared.Database.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.Database.SqlServer.Context;

internal sealed class SqlServerReadContext : DbContext
{
    private readonly SqlServerConnectionString _sqlServerConnectionString;

    public SqlServerReadContext(
        DbContextOptions<SqlServerReadContext> options,
        SqlServerConnectionString sqlServerConnectionString
    )
        : base(options)
    {
        _sqlServerConnectionString = sqlServerConnectionString;
    }

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
        type.FullName?.Contains("Configurations.Read") ?? false;

    public DbSet<UserModel> Users { get; set; }

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransferModel> Transfers { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }
}

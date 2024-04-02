using Domain.Account.Models;
using Domain.Transfer.Models;
using Domain.User.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.Database.SqlServer.Context;

public class SqlServerReadContext : DbContext
{
    public SqlServerReadContext(DbContextOptions options) : 
        base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerWriteContext).Assembly, WriteConfigurationsFilter);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;User Id=sa;Password=@Sdfl29052003;Encrypt=True;Database=PROJETO_DB;TrustServerCertificate=True"
        );
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;

    public DbSet<UserModel> Users { get; set; }

    public DbSet<UserAddressModel> Addresses { get; set; }

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransferModel> Transfers { get; set; }
}

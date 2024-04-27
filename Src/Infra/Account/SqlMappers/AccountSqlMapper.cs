using Domain.Account.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Account.SqlMappers;

public sealed class AccountSqlMapper : IEntityTypeConfiguration<AccountModel>
{
    public void Configure(EntityTypeBuilder<AccountModel> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(account => account.Id);
        builder
            .Property(account => account.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder.HasIndex(p => p.UserId).IsUnique();
        builder
            .HasOne(account => account.User)
            .WithOne(account => account.Account)
            .HasForeignKey<AccountModel>(p => p.UserId);

        builder.Navigation(account => account.Transactions).AutoInclude();
    }
}

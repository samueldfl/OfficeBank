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
    }
}

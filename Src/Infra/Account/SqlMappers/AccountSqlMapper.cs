using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Account.Models;

namespace Infra.Account.SqlMappers;

public sealed class AccountSqlMapper : IEntityTypeConfiguration<AccountModel>
{
    public void Configure(EntityTypeBuilder<AccountModel> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder.Property(p => p.Amount).HasColumnType("decimal").HasDefaultValue(0);

        builder.HasIndex(p => p.UserId).IsUnique();
        builder
            .HasOne(p => p.User)
            .WithOne(p => p.Account)
            .HasForeignKey<AccountModel>(p => p.UserId);
    }
}

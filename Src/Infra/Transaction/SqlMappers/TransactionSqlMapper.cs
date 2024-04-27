using Domain.Payment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Transaction.SqlMappers;

internal class TransactionSqlMapper : IEntityTypeConfiguration<TransactionModel>
{
    public void Configure(EntityTypeBuilder<TransactionModel> builder)
    {
        builder
            .Property(transaction => transaction.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder.Property(transaction => transaction.Balance).HasPrecision(19, 5);

        builder.Property(transaction => transaction.LastBalance).HasPrecision(19, 5);

        builder.Property(transaction => transaction.BalanceDiff).HasPrecision(19, 5);

        builder
            .Property(transaction => transaction.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder
            .HasOne(transaction => transaction.Account)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Transfer.Models;

namespace Infra.Account.SqlMappers;

public sealed class TransferSqlMapper : IEntityTypeConfiguration<TransferModel>
{
    public void Configure(EntityTypeBuilder<TransferModel> builder)
    {
        builder.ToTable("Transfers");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder
            .HasOne(p => p.FromAccount)
            .WithMany(p => p.Transfers)
            .HasForeignKey(p => p.FromAccountId);

        builder
            .HasOne(p => p.ToAccount)
            .WithMany(p => p.Transfers)
            .HasForeignKey(p => p.ToAccountId);

        builder.HasMany(p => p.Accounts).WithMany(p => p.Transfers);
    }
}

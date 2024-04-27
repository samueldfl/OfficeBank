using Domain.Transfer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Account.SqlMappers;

public sealed class TransferSqlMapper : IEntityTypeConfiguration<TransferModel>
{
    public void Configure(EntityTypeBuilder<TransferModel> builder)
    {
        builder.ToTable("Transfers");

        builder.HasKey(transfer => transfer.Id);
        builder
            .Property(transfer => transfer.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(transfer => transfer.ToAccountId)
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder
            .Property(transfer => transfer.FromAccountId)
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(transfer => transfer.Amount).HasPrecision(19, 5);
    }
}

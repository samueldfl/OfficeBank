using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.User.Models;

namespace Infra.User.SqlMappers;

public sealed class UserSqlMapper : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);
        builder
            .Property(user => user.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(user => user.Name)
            .HasColumnType("varchar")
            .HasMaxLength(60)
            .IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.Email).HasColumnType("varchar").HasMaxLength(256);

        builder.Property(user => user.Password).HasColumnType("varchar").HasMaxLength(60);

        builder.HasIndex(user => user.CPF).IsUnique();
        builder.Property(user => user.CPF).HasColumnType("varchar").HasMaxLength(60);

        builder
            .Property(user => user.CreatedAt)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder
            .Property(user => user.UpdatedAt)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.Property(user => user.DeleteAt).HasColumnType("datetime2");
        builder.HasQueryFilter(user => user.DeleteAt != null);

        builder.OwnsOne(
            address => address.Address,
            builder =>
            {
                builder
                    .Property(address => address.Id)
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");

                builder
                    .Property(address => address.Street)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .Property(address => address.Number)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .Property(address => address.ZipCode)
                    .HasColumnType("varchar")
                    .HasMaxLength(8)
                    .IsRequired();

                builder
                    .Property(address => address.Neighborhood)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .Property(address => address.City)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .Property(address => address.State)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .Property(address => address.Country)
                    .HasColumnType("varchar")
                    .IsRequired();

                builder
                    .WithOwner(address => address.User)
                    .HasForeignKey(address => address.UserId);
            }
        );
    }
}

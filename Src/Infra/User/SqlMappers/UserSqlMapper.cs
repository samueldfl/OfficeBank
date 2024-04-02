using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.User.Models;

namespace Infra.User.SqlMappers;

public sealed class UserSqlMapper : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()");

        builder.Property(p => p.Name).HasColumnType("varchar(40)").IsRequired();

        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Email).HasColumnType("varchar(60)");

        builder.Property(p => p.Password).HasColumnType("varchar(60)");

        builder.HasIndex(p => p.CPF).IsUnique();
        builder.Property(p => p.CPF).HasColumnType("varchar(60)");

        builder
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder
            .Property(p => p.UpdatedAt)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.Property(p => p.DeleteAt).HasColumnType("datetime2");
    }
}

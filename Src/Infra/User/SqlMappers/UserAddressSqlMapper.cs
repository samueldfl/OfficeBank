using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.User.Models;

namespace Infra.User.SqlMappers;

public sealed class UserAddressSqlMapper : IEntityTypeConfiguration<UserAddressModel>
{
    public void Configure(EntityTypeBuilder<UserAddressModel> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(p => p.UserId);
        builder
            .HasOne(p => p.User)
            .WithOne(p => p.Address)
            .HasForeignKey<UserAddressModel>(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

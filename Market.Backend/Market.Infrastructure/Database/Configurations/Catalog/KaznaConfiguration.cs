using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class KaznaConfiguration
    : IEntityTypeConfiguration<Kazna>
{
    public void Configure(EntityTypeBuilder<Kazna> builder)
    {
        builder.HasOne(x => x.Borrow)
            .WithMany(x => x.Kazne)
            .HasForeignKey(x => x.BorrowId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
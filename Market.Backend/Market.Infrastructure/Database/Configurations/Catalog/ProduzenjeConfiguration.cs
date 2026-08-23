using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class ProduzenjeConfiguration
    : IEntityTypeConfiguration<Produzenje>
{
    public void Configure(EntityTypeBuilder<Produzenje> builder)
    {
        builder.HasOne(x => x.Borrow)
            .WithMany(x => x.Produzenja)
            .HasForeignKey(x => x.BorrowId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Admin)
            .WithMany()
            .HasForeignKey(x => x.AdminId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
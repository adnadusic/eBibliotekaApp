using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class WishlistConfiguration
    : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("ListeZelja");

        builder.Property(x => x.AddedAt)
            .HasColumnName("DatumDodavanja");

        builder.Property(x => x.Priority)
            .HasColumnName("Prioritet");

        builder.Property(x => x.Note)
            .HasColumnName("Napomena");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Wishlists)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
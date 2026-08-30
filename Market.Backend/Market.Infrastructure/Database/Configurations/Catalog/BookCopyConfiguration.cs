using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class BookCopyConfiguration
    : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Primjerci");

        builder.Property(x => x.InventoryNumber)
            .HasColumnName("InventarniBroj");

        builder.Property(x => x.Condition)
            .HasColumnName("Stanje");

        builder.Property(x => x.ShelfLocation)
            .HasColumnName("LokacijaPolice");

        builder.Property(x => x.AcquisitionDate)
            .HasColumnName("DatumNabavke");

        builder.Property(x => x.Note)
            .HasColumnName("Napomena");

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Copies)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
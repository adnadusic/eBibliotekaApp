using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class BookConfiguration
    : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Knjige");

        builder.Property(x => x.Title)
            .HasColumnName("Naslov");

        builder.Property(x => x.PublicationYear)
            .HasColumnName("GodinaIzdanja");

        builder.Property(x => x.PageCount)
            .HasColumnName("BrojStranica");

        builder.Property(x => x.LanguageId)
            .HasColumnName("JezikId");

        builder.Property(x => x.Description)
            .HasColumnName("Opis");

        builder.Property(x => x.CoverImage)
            .HasColumnName("SlikaKorice");

        builder.Property(x => x.TotalCopies)
            .HasColumnName("UkupnoPrimjeraka");

        builder.Property(x => x.AvailableCopies)
            .HasColumnName("DostupnoPrimjeraka");

        builder.Property(x => x.AverageRating)
            .HasColumnName("ProsjecnaOcjena");

        builder.Property(x => x.RatingCount)
            .HasColumnName("BrojOcjena");

        builder.Property(x => x.ViewCount)
            .HasColumnName("BrojPregleda");

        builder.Property(x => x.AddedAt)
            .HasColumnName("DatumDodavanja");

        builder.HasIndex(x => x.Isbn)
            .IsUnique();

        builder.Property(x => x.Isbn)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(x => x.Publisher)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.PublisherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
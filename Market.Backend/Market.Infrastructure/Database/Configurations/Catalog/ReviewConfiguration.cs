using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class ReviewConfiguration
    : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Recenzije");

        builder.Property(x => x.Rating)
            .HasColumnName("Ocjena");

        builder.Property(x => x.Title)
            .HasColumnName("Naslov");

        builder.Property(x => x.Comment)
            .HasColumnName("Komentar");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("Datum");

        builder.Property(x => x.HelpfulCount)
            .HasColumnName("BrojHelpful");

        builder.Property(x => x.UnhelpfulCount)
            .HasColumnName("BrojUnhelpful");

        builder.Property(x => x.IsEdited)
            .HasColumnName("Izmijenjeno");

        builder.Property(x => x.EditedAt)
            .HasColumnName("DatumIzmjene");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
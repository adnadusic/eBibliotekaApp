using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class BookAuthorConfiguration
    : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("KnjigaAutori");

        builder.Property(x => x.ContributionType)
            .HasColumnName("TipDoprinosa");

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Authors)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
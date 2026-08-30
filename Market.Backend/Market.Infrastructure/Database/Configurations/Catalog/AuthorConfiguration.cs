using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Autori");

        builder.Property(x => x.FirstName)
            .HasColumnName("Ime");

        builder.Property(x => x.LastName)
            .HasColumnName("Prezime");

        builder.Property(x => x.Biography)
            .HasColumnName("Biografija");

        builder.Property(x => x.DateOfBirth)
            .HasColumnName("DatumRodjenja");

        builder.Property(x => x.Country)
            .HasColumnName("Zemlja");

        builder.Property(x => x.Image)
            .HasColumnName("Slika");
    }
}

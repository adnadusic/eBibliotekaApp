using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Zanrovi");

        builder.Property(x => x.Name)
            .HasColumnName("Naziv");

        builder.Property(x => x.Description)
            .HasColumnName("Opis");
    }
}

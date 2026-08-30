using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Gradovi");

        builder.Property(x => x.Name)
            .HasColumnName("Naziv");

        builder.Property(x => x.PostalCode)
            .HasColumnName("PostanskiBroj");
    }
}

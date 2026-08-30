using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Izdavaci");

        builder.Property(x => x.Name)
            .HasColumnName("Naziv");

        builder.Property(x => x.Address)
            .HasColumnName("Adresa");

        builder.Property(x => x.Phone)
            .HasColumnName("Telefon");
    }
}

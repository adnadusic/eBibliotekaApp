using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Jezici");

        builder.Property(x => x.Name)
            .HasColumnName("Naziv");

        builder.Property(x => x.Code)
            .HasColumnName("Kod");
    }
}

using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
{
    public void Configure(EntityTypeBuilder<SystemSetting> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("SistemskePostavke");

        builder.Property(x => x.Name)
            .HasColumnName("Naziv");

        builder.Property(x => x.Value)
            .HasColumnName("Vrijednost");

        builder.Property(x => x.Type)
            .HasColumnName("Tip");

        builder.Property(x => x.Description)
            .HasColumnName("Opis");

        builder.Property(x => x.ModifiedAt)
            .HasColumnName("Izmijenjeno");
    }
}

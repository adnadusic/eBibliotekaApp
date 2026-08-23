using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class KnjigaConfiguration
    : IEntityTypeConfiguration<Knjiga>
{
    public void Configure(EntityTypeBuilder<Knjiga> builder)
    {
        builder.HasIndex(x => x.Isbn)
            .IsUnique();

        builder.Property(x => x.Isbn)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(x => x.Publisher)
            .WithMany(x => x.Knjige)
            .HasForeignKey(x => x.PublisherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Jezik)
            .WithMany()
            .HasForeignKey(x => x.JezikId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
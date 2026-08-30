using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class NotificationConfiguration
    : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Obavijesti");

        builder.Property(x => x.Type)
            .HasColumnName("Tip");

        builder.Property(x => x.Title)
            .HasColumnName("Naslov");

        builder.Property(x => x.Message)
            .HasColumnName("Poruka");

        builder.Property(x => x.SentAt)
            .HasColumnName("DatumSlanja");

        builder.Property(x => x.IsRead)
            .HasColumnName("Procitano");

        builder.Property(x => x.ReadAt)
            .HasColumnName("DatumCitanja");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
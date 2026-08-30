using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class ReservationConfiguration
    : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Rezervacije");

        builder.Property(x => x.ReservationDate)
            .HasColumnName("DatumRezervacije");

        builder.Property(x => x.ExpirationDate)
            .HasColumnName("DatumIsteka");

        builder.Property(x => x.Priority)
            .HasColumnName("Prioritet");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
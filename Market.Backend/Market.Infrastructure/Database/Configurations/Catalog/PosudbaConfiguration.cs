using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class PosudbaConfiguration
    : IEntityTypeConfiguration<Posudba>
{
    public void Configure(EntityTypeBuilder<Posudba> builder)
    {
        builder.HasOne(x => x.Reservation)
            .WithMany(x => x.Posudbe)
            .HasForeignKey(x => x.ReservationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Copy)
            .WithMany(x => x.Posudbe)
            .HasForeignKey(x => x.CopyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
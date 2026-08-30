using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class LoanConfiguration
    : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Posudbe");

        builder.Property(x => x.LoanDate)
            .HasColumnName("DatumPosudbe");

        builder.Property(x => x.DueDate)
            .HasColumnName("PlaniraniDatumVracanja");

        builder.Property(x => x.ReturnDate)
            .HasColumnName("DatumVracanja");

        builder.Property(x => x.IsExtended)
            .HasColumnName("Produzena");

        builder.Property(x => x.ExtensionCount)
            .HasColumnName("BrojProduzenja");

        builder.Property(x => x.ConditionAtPickup)
            .HasColumnName("StanjePriPreuzimanju");

        builder.Property(x => x.ConditionAtReturn)
            .HasColumnName("StanjePriVracanju");

        builder.Property(x => x.Note)
            .HasColumnName("Napomena");

        builder.HasOne(x => x.Reservation)
            .WithMany(x => x.Loans)
            .HasForeignKey(x => x.ReservationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Copy)
            .WithMany(x => x.Loans)
            .HasForeignKey(x => x.CopyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
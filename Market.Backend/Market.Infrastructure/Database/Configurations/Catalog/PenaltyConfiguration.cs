using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class PenaltyConfiguration
    : IEntityTypeConfiguration<Penalty>
{
    public void Configure(EntityTypeBuilder<Penalty> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Kazne");

        builder.Property(x => x.LoanId)
            .HasColumnName("BorrowId");

        builder.Property(x => x.PenaltyType)
            .HasColumnName("TipKazne");

        builder.Property(x => x.Amount)
            .HasColumnName("Iznos");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("DatumNastanka");

        builder.Property(x => x.PaidAt)
            .HasColumnName("DatumPlacanja");

        builder.Property(x => x.Description)
            .HasColumnName("Opis");

        builder.Property(x => x.OverdueDays)
            .HasColumnName("BrojDanaKasnjenja");

        builder.HasOne(x => x.Loan)
            .WithMany(x => x.Fines)
            .HasForeignKey(x => x.LoanId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
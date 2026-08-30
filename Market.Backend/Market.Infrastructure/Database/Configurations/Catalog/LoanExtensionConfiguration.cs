using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class LoanExtensionConfiguration
    : IEntityTypeConfiguration<LoanExtension>
{
    public void Configure(EntityTypeBuilder<LoanExtension> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("Produzenja");

        builder.Property(x => x.LoanId)
            .HasColumnName("BorrowId");

        builder.Property(x => x.RequestDate)
            .HasColumnName("DatumZahtjeva");

        builder.Property(x => x.NewDueDate)
            .HasColumnName("NoviDatumVracanja");

        builder.Property(x => x.Reason)
            .HasColumnName("Razlog");

        builder.Property(x => x.ProcessedAt)
            .HasColumnName("DatumObrade");

        builder.Property(x => x.Note)
            .HasColumnName("Napomena");

        builder.HasOne(x => x.Loan)
            .WithMany(x => x.Extensions)
            .HasForeignKey(x => x.LoanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Admin)
            .WithMany()
            .HasForeignKey(x => x.AdminId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
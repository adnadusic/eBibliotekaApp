using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class UserManagementLogConfiguration
    : IEntityTypeConfiguration<UserManagementLog>
{
    public void Configure(EntityTypeBuilder<UserManagementLog> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("UpravljanjeKorisnicima");

        builder.Property(x => x.ActionType)
            .HasColumnName("TipAkcije");

        builder.Property(x => x.Reason)
            .HasColumnName("Razlog");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("Datum");

        builder.Property(x => x.DurationDays)
            .HasColumnName("TrajanjeDana");

        builder.Property(x => x.AdditionalNotes)
            .HasColumnName("DodatneNapomene");

        builder.HasOne(x => x.Admin)
            .WithMany()
            .HasForeignKey(x => x.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TargetUser)
            .WithMany()
            .HasForeignKey(x => x.TargetUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
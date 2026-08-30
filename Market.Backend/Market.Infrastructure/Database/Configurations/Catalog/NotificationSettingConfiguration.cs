using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class NotificationSettingConfiguration
    : IEntityTypeConfiguration<NotificationSetting>
{
    public void Configure(EntityTypeBuilder<NotificationSetting> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("PostavkeObavijesti");

        builder.Property(x => x.Type)
            .HasColumnName("Tip");

        builder.Property(x => x.IsPriority)
            .HasColumnName("Prioritetna");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.Type
        })
        .IsUnique();
    }
}
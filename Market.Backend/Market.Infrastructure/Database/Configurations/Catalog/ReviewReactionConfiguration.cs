using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class ReviewReactionConfiguration
    : IEntityTypeConfiguration<ReviewReaction>
{
    public void Configure(EntityTypeBuilder<ReviewReaction> builder)
    {
        // Preserve the existing database schema while using English domain names.
        builder.ToTable("OcjeneRecenzija");

        builder.Property(x => x.ReactionType)
            .HasColumnName("TipOcjene");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("Datum");

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Review)
            .WithMany(x => x.Reactions)
            .HasForeignKey(x => x.ReviewId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
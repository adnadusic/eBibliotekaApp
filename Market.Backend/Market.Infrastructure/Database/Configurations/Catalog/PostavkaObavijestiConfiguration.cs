using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class PostavkaObavijestiConfiguration
    : IEntityTypeConfiguration<PostavkaObavijesti>
{
    public void Configure(EntityTypeBuilder<PostavkaObavijesti> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.Tip
        })
        .IsUnique();
    }
}
using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class KnjigaZanrConfiguration
    : IEntityTypeConfiguration<KnjigaZanr>
{
    public void Configure(EntityTypeBuilder<KnjigaZanr> builder)
    {
        builder.HasOne(x => x.Knjiga)
            .WithMany(x => x.Zanrovi)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Zanr)
            .WithMany(x => x.Knjige)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
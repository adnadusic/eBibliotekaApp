using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class KnjigaAutorConfiguration
    : IEntityTypeConfiguration<KnjigaAutor>
{
    public void Configure(EntityTypeBuilder<KnjigaAutor> builder)
    {
        builder.HasOne(x => x.Knjiga)
            .WithMany(x => x.Autori)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Autor)
            .WithMany(x => x.Knjige)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
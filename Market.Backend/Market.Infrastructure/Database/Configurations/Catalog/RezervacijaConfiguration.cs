using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class RezervacijaConfiguration
    : IEntityTypeConfiguration<Rezervacija>
{
    public void Configure(EntityTypeBuilder<Rezervacija> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Rezervacije)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
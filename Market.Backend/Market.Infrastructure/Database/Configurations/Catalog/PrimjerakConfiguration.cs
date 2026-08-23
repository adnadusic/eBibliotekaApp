using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Infrastructure.Database.Configurations.Catalog;

public sealed class PrimjerakConfiguration
    : IEntityTypeConfiguration<Primjerak>
{
    public void Configure(EntityTypeBuilder<Primjerak> builder)
    {
        builder.HasOne(x => x.Book)
            .WithMany(x => x.Primerci)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
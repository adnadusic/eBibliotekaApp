using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Database.Seeders;

public partial class StaticDataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grad>().HasData(
            new Grad
            {
                Id = 1,
                Naziv = "Mostar",
                PostanskiBroj = "88000",
                IsDeleted = false,
                CreatedAtUtc = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Jezik>().HasData(
            new Jezik
            {
                Id = 1,
                Naziv = "Bosanski",
                Kod = "bs",
                IsDeleted = false,
                CreatedAtUtc = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
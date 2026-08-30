using Market.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Database.Seeders;

public partial class StaticDataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City
            {
                Id = 1,
                Name = "Mostar",
                PostalCode = "88000",
                IsDeleted = false,
                CreatedAtUtc = new DateTime(
                    2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Language>().HasData(
            new Language
            {
                Id = 1,
                Name = "Bosnian",
                Code = "bs",
                IsDeleted = false,
                CreatedAtUtc = new DateTime(
                    2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
namespace Market.Infrastructure.Database.Seeders;

/// <summary>
/// Dynamic seeder that runs at application startup.
/// Used for inserting demo/test data.
/// </summary>
public static class DynamicDataSeeder
{
    public static async Task SeedAsync(DatabaseContext context)
    {
        await SeedUsersAsync(context);
    }

    /// <summary>
    /// Creates demo users if they do not already exist.
    /// </summary>
    private static async Task SeedUsersAsync(DatabaseContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var hasher = new PasswordHasher<MarketUserEntity>();

        var admin = new MarketUserEntity
        {
            Email = "admin@ebiblioteka.local",
            PasswordHash = hasher.HashPassword(null!, "Admin123!"),

            FirstName = "Admin",
            LastName = "Biblioteka",
            PhoneNumber = "000000000",
            Address = "e-Biblioteka",

            IsAdmin = true,
            IsEnabled = true,
        };

        var user = new MarketUserEntity
        {
            Email = "user@ebiblioteka.local",
            PasswordHash = hasher.HashPassword(null!, "User123!"),

            FirstName = "Demo",
            LastName = "Korisnik",
            PhoneNumber = "000000001",
            Address = "e-Biblioteka",

            IsEnabled = true,
        };

        var dummyForSwagger = new MarketUserEntity
        {
            Email = "string",
            PasswordHash = hasher.HashPassword(null!, "string"),

            FirstName = "Swagger",
            LastName = "User",
            PhoneNumber = "000000002",
            Address = "e-Biblioteka",

            IsEnabled = true,
        };

        var dummyForTests = new MarketUserEntity
        {
            Email = "test",
            PasswordHash = hasher.HashPassword(null!, "test123"),

            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "000000003",
            Address = "e-Biblioteka",

            IsEnabled = true,
        };

        context.Users.AddRange(
            admin,
            user,
            dummyForSwagger,
            dummyForTests
        );

        await context.SaveChangesAsync();

        Console.WriteLine("Dynamic seed: e-Biblioteka demo users added.");
    }
}
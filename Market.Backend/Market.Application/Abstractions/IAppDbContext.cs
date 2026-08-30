namespace Market.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    DbSet<MarketUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }

    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<BookCopy> BookCopies { get; set; }
    DbSet<Publisher> Publishers { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<Language> Languages { get; set; }
    DbSet<City> Cities { get; set; }

    DbSet<BookAuthor> BookAuthors { get; set; }
    DbSet<BookGenre> BookGenres { get; set; }

    DbSet<Reservation> Reservations { get; set; }
    DbSet<Loan> Loans { get; set; }
    DbSet<LoanExtension> LoanExtensions { get; set; }

    DbSet<Review> Reviews { get; set; }
    DbSet<ReviewReaction> ReviewReactions { get; set; }

    DbSet<Penalty> Penalties { get; set; }
    DbSet<Wishlist> Wishlists { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<NotificationSetting> NotificationSettings { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }

    DbSet<SystemSetting> SystemSettings { get; set; }
    DbSet<UserManagementLog> UserManagementLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
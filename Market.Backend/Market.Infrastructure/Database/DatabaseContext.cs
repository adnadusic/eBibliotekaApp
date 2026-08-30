using Market.Application.Abstractions;

namespace Market.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{
    public DbSet<MarketUserEntity> Users => Set<MarketUserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<BookCopy> BookCopies { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    public DbSet<BookAuthor> BookAuthors { get; set; } = null!;
    public DbSet<BookGenre> BookGenres { get; set; } = null!;

    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<Loan> Loans { get; set; } = null!;
    public DbSet<LoanExtension> LoanExtensions { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ReviewReaction> ReviewReactions { get; set; } = null!;

    public DbSet<Penalty> Penalties { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<NotificationSetting> NotificationSettings { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    public DbSet<SystemSetting> SystemSettings { get; set; } = null!;
    public DbSet<UserManagementLog> UserManagementLogs { get; set; } = null!;

    private readonly TimeProvider _clock;
    private readonly IAppCurrentUser _currentUser;

    public DatabaseContext(
        DbContextOptions<DatabaseContext> options,
        TimeProvider clock,
        IAppCurrentUser currentUser)
        : base(options)
    {
        _clock = clock;
        _currentUser = currentUser;
    }
}
using Market.Application.Abstractions;

namespace Market.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{
    public DbSet<MarketUserEntity> Users => Set<MarketUserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    public DbSet<Korisnik> Korisnici { get; set; } = null!;
    public DbSet<Knjiga> Knjige { get; set; } = null!;
    public DbSet<Autor> Autori { get; set; } = null!;
    public DbSet<Primjerak> Primjerci { get; set; } = null!;
    public DbSet<Posudba> Posudbe { get; set; } = null!;
    public DbSet<Rezervacija> Rezervacije { get; set; } = null!;
    public DbSet<Recenzija> Recenzije { get; set; } = null!;
    public DbSet<Kazna> Kazne { get; set; } = null!;
    public DbSet<ListaZelja> ListeZelja { get; set; } = null!;
    public DbSet<KnjigaAutor> KnjigaAutori { get; set; } = null!;
    public DbSet<KnjigaZanr> KnjigaZanrovi { get; set; } = null!;
    public DbSet<Zanr> Zanrovi { get; set; } = null!;
    public DbSet<SistemskePostavke> SistemskePostavke { get; set; } = null!;
    public DbSet<UpravljanjeKorisnicima> UpravljanjeKorisnicima { get; set; } = null!;

    private readonly TimeProvider _clock;

    public DatabaseContext(
        DbContextOptions<DatabaseContext> options,
        TimeProvider clock)
        : base(options)
    {
        _clock = clock;
    }
}
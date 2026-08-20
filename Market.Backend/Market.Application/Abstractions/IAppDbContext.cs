namespace Market.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    
    DbSet<MarketUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }

    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Knjiga> Knjige { get; set; }
    public DbSet<Autor> Autori { get; set; }
    public DbSet<Primjerak> Primjerci { get; set; }
    public DbSet<Posudba> Posudbe { get; set; }
    public DbSet<Rezervacija> Rezervacije { get; set; }
    public DbSet<Recenzija> Recenzije { get; set; }
    public DbSet<Kazna> Kazne { get; set; }
    public DbSet<ListaZelja> ListeZelja { get; set; }
    public DbSet<KnjigaAutor> KnjigaAutori { get; set; }
    public DbSet<KnjigaZanr> KnjigaZanrovi { get; set; }
    public DbSet<Zanr> Zanrovi { get; set; }
    public DbSet<SistemskePostavke> SistemskePostavke { get; set; }
    public DbSet<UpravljanjeKorisnicima> UpravljanjeKorisnicima { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
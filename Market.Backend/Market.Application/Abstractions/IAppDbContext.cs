namespace Market.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
    DbSet<MarketUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }

    DbSet<Knjiga> Knjige { get; set; }
    DbSet<Autor> Autori { get; set; }
    DbSet<Primjerak> Primjerci { get; set; }
    DbSet<Izdavac> Izdavaci { get; set; }
    DbSet<Zanr> Zanrovi { get; set; }
    DbSet<Jezik> Jezici { get; set; }
    DbSet<Grad> Gradovi { get; set; }

    DbSet<KnjigaAutor> KnjigaAutori { get; set; }
    DbSet<KnjigaZanr> KnjigaZanrovi { get; set; }

    DbSet<Rezervacija> Rezervacije { get; set; }
    DbSet<Posudba> Posudbe { get; set; }
    DbSet<Produzenje> Produzenja { get; set; }

    DbSet<Recenzija> Recenzije { get; set; }
    DbSet<OcjenaRecenzije> OcjeneRecenzija { get; set; }

    DbSet<Kazna> Kazne { get; set; }
    DbSet<ListaZelja> ListeZelja { get; set; }
    DbSet<Obavijest> Obavijesti { get; set; }

    DbSet<SistemskePostavke> SistemskePostavke { get; set; }
    DbSet<UpravljanjeKorisnicima> UpravljanjeKorisnicima { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
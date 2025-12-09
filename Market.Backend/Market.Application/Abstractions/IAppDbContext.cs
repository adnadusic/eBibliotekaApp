using Market.Domain.Entities.Catalog;
using Market.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MarketUserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DbSet<Autor> Autori { get; set; }
        public DbSet<Izdavac> Izdavaci { get; set; }
        public DbSet<Knjiga> Knjige { get; set; }
        public DbSet<Primjerak> Primjerci { get; set; }
        public DbSet<Zanr> Zanrovi { get; set; }
        public DbSet<Jezik> Jezici { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<KnjigaAutor> KnjigaAutori { get; set; }
        public DbSet<KnjigaZanr> KnjigaZanrovi { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<OcjenaRecenzije> OcjeneRecenzija { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Posudba> Posudbe { get; set; }
        public DbSet<Produzenje> Produzenja { get; set; }
        public DbSet<ListaZelja> ListeZelja { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }
        public DbSet<Kazna> Kazne { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Knjiga
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Isbn { get; set; }
        public int? GodinaIzdanja { get; set; }
        public int? BrojStranica { get; set; }
        public string Jezik { get; set; }
        public string Opis { get; set; }
        public string SlikaKorice { get; set; }
        public int? PublisherId { get; set; }
        public int? UkupnoPrimjeraka { get; set; }
        public int? DostupnoPrimjeraka { get; set; }
        public decimal? ProsjecnaOcjena { get; set; }
        public int? BrojOcjena { get; set; }
        public int? BrojPregleda { get; set; }
        public DateTime? DatumDodavanja { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Izdavac Publisher { get; set; }

        public ICollection<Primjerak> Primerci { get; set; }
        public ICollection<KnjigaAutor> Autori { get; set; }
        public ICollection<KnjigaZanr> Zanrovi { get; set; }
        public ICollection<Recenzija> Recenzije { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }
        public ICollection<ListaZelja> ListeZelja { get; set; }
    }
}

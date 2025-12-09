using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public enum TipStanjaKnjige
    {
        Novo,
        Dobro,
        Osteceno
    }

    public sealed class Knjiga : BaseEntity
    {
        public string Naslov { get; set; }
        public string Isbn { get; set; }
        public int? GodinaIzdanja { get; set; }
        public int? BrojStranica { get; set; }
        public string Opis { get; set; }
        public string SlikaKorice { get; set; }
        public DateTime? DatumDodavanja { get; set; }

        public int? PublisherId { get; set; }
        public Izdavac Publisher { get; set; }

        // VEZA S DRUGIM ENTITETIMA
        public ICollection<Primjerak> Primerci { get; set; }
        public ICollection<KnjigaAutor> Autori { get; set; }
        public ICollection<KnjigaZanr> Zanrovi { get; set; }
        public ICollection<Recenzija> Recenzije { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }
        public ICollection<ListaZelja> ListeZelja { get; set; }
    }
}

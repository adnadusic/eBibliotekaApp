using Market.Domain.Common;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public enum StatusPrimjerka
    {
        Dostupan,
        Posudjen,
        Izgubljen,
        Rezervisan
    }

    public sealed class Primjerak : BaseEntity
    {
        public string Sifra { get; set; } // inventarski broj

        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; }

        public StatusPrimjerka Status { get; set; } = StatusPrimjerka.Dostupan;

        public ICollection<Posudba> Posudbe { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }
    }
}

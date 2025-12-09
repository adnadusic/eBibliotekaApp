using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;

namespace Market.Domain.Entities.Catalog
{
    public enum StatusRezervacije
    {
        Aktivna,
        Istekla,
        Otkazana
    }

    public sealed class Rezervacija : BaseEntity
    {
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        public int PrimjerakId { get; set; }
        public Primjerak Primjerak { get; set; }

        public DateTime DatumRezervacije { get; set; }
        public DateTime Istice { get; set; } 

        public DateTime? DatumPreuzimanja { get; set; } 

        public StatusRezervacije Status { get; set; } = StatusRezervacije.Aktivna;
    }
}

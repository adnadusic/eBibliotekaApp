using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public enum StatusRecenzije
    {
        Objavljena,
        Uklonjena
    }

    public sealed class Recenzija : BaseEntity
    {
        public string Naslov { get; set; }
        public string Tekst { get; set; }
        public int Ocjena { get; set; } // 1–5
        public DateTime DatumObjave { get; set; }

        public StatusRecenzije Status { get; set; } = StatusRecenzije.Objavljena;

        // FK prema korisniku
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        // FK prema knjizi
        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; }

        // dodatno: korisnici mogu ocjenjivati recenziju (lajkovi)
        public ICollection<OcjenaRecenzije> Ocjene { get; set; }
    }
}

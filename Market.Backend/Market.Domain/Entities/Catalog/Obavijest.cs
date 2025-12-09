using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;

namespace Market.Domain.Entities.Catalog
{
    public sealed class Obavijest : BaseEntity
    {
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        public string Naslov { get; set; }
        public string Tekst { get; set; }

        public DateTime DatumSlanja { get; set; }

        public bool Procitano { get; set; } = false;
        public DateTime? DatumCitanja { get; set; }

        public int? VezanoZaId { get; set; }

        public TipObavijesti Tip { get; set; } = TipObavijesti.Sistemska;
    }

    public enum TipObavijesti
    {
        Sistemska,
        Podsjetnik
    }
}

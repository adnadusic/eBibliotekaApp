using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;

namespace Market.Domain.Entities.Catalog
{
    public enum StatusPosudbe
    {
        Aktivna,
        Vracen,
        Zakasnjenje,
        Izgubljena
    }

    public sealed class Posudba : BaseEntity
    {
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        public int PrimjerakId { get; set; }
        public Primjerak Primjerak { get; set; }

        public DateTime DatumPosudbe { get; set; }
        public DateTime RokZaVracanje { get; set; }
        public DateTime? DatumVracanja { get; set; }

        public StatusPosudbe Status { get; set; } = StatusPosudbe.Aktivna;

        // Ako bude produženja posudbe
        public ICollection<Produzenje> Produzenja { get; set; }
    }
}

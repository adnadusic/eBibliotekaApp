using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;

namespace Market.Domain.Entities.Catalog
{
    public enum StatusKazne
    {
        Aktivna,
        Placena
    }

    public sealed class Kazna : BaseEntity
    {
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        public decimal Iznos { get; set; } 
        public DateTime DatumEvidentiranja { get; set; }
        public DateTime? DatumPlacanja { get; set; }

        public StatusKazne Status { get; set; } = StatusKazne.Aktivna;
    }
}

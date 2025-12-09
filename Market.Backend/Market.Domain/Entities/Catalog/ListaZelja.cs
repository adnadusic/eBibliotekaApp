using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System;

namespace Market.Domain.Entities.Catalog
{
    public sealed class ListaZelja : BaseEntity
    {
        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; }

        public DateTime DatumDodavanja { get; set; }
    }
}

using Market.Domain.Common;
using System;

namespace Market.Domain.Entities.Catalog
{
    public enum TipDoprinosa
    {
        GlavniAutor,
        Koautor,
        Urednik,
        Ilustrator
    }

    public sealed class KnjigaAutor : BaseEntity
    {
        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public TipDoprinosa TipDoprinosa { get; set; }
    }
}

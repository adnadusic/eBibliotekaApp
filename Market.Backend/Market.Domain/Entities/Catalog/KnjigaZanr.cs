using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public sealed class KnjigaZanr : BaseEntity
    {
        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; }

        public int ZanrId { get; set; }
        public Zanr Zanr { get; set; }
    }
}

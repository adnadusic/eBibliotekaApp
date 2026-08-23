using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class KnjigaZanr : BaseEntity
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }

        public Knjiga Knjiga { get; set; }
        public Zanr Zanr { get; set; }
    }
}
using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class KnjigaAutor : BaseEntity
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public ContributionType TipDoprinosa { get; set; }

        public Knjiga Knjiga { get; set; }
        public Autor Autor { get; set; }
    }
}
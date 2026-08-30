using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class BookGenre : BaseEntity
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }

        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
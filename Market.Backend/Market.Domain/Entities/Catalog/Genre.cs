using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BookGenre> Books { get; set; }
    }
}
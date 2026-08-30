using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class BookAuthor : BaseEntity
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public ContributionType ContributionType { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }

        public ICollection<BookAuthor> Books { get; set; }
    }
}
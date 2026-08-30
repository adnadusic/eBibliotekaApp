using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public class Wishlist : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime AddedAt { get; set; }
        public int? Priority { get; set; }
        public string Note { get; set; }

        public MarketUserEntity User { get; set; }
        public Book Book { get; set; }
    }
}
using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ReservationStatus Status { get; set; }
        public int? Priority { get; set; }

        public MarketUserEntity User { get; set; }
        public Book Book { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
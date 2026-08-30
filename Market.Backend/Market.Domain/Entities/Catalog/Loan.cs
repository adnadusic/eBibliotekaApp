using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public class Loan : BaseEntity
    {
        public int? ReservationId { get; set; }
        public int CopyId { get; set; }
        public int UserId { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool? IsExtended { get; set; }
        public int? ExtensionCount { get; set; }

        public string ConditionAtPickup { get; set; }
        public string ConditionAtReturn { get; set; }
        public string Note { get; set; }

        public BookCopy Copy { get; set; }
        public MarketUserEntity User { get; set; }
        public Reservation? Reservation { get; set; }

        public ICollection<Penalty> Fines { get; set; }
        public ICollection<LoanExtension> Extensions { get; set; }
    }
}
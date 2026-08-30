using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Penalty : BaseEntity
    {
        public int? LoanId { get; set; }
        public int UserId { get; set; }

        public PenaltyType PenaltyType { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public PenaltyStatus Status { get; set; }
        public string Description { get; set; }
        public int? OverdueDays { get; set; }

        public Loan? Loan { get; set; }
        public MarketUserEntity User { get; set; }
    }
}
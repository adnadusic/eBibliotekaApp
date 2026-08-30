using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class LoanExtension : BaseEntity
    {
        public int LoanId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime NewDueDate { get; set; }
        public string Reason { get; set; }
        public ExtensionStatus Status { get; set; }
        public int? AdminId { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string Note { get; set; }

        public Loan Loan { get; set; }
        public MarketUserEntity? Admin { get; set; }
    }
}
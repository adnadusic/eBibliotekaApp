using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class BookCopy : BaseEntity
    {
        public int BookId { get; set; }
        public string InventoryNumber { get; set; }
        public string Condition { get; set; }
        public string ShelfLocation { get; set; }
        public BookCopyStatus Status { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public string Note { get; set; }

        public Book Book { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
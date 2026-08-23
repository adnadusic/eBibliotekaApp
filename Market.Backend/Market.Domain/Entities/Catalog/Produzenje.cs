using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Produzenje : BaseEntity
    {
        public int BorrowId { get; set; }
        public DateTime DatumZahtjeva { get; set; }
        public DateTime NoviDatumVracanja { get; set; }
        public string Razlog { get; set; }
        public ExtensionStatus Status { get; set; }
        public int? AdminId { get; set; }
        public DateTime? DatumObrade { get; set; }
        public string Napomena { get; set; }

        public Posudba Borrow { get; set; }
        public MarketUserEntity? Admin { get; set; }
    }
}
using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Kazna : BaseEntity
    {
        public int? BorrowId { get; set; }
        public int UserId { get; set; }
        public PenaltyType TipKazne { get; set; }
        public decimal Iznos { get; set; }
        public DateTime DatumNastanka { get; set; }
        public DateTime? DatumPlacanja { get; set; }
        public PenaltyStatus Status { get; set; }
        public string Opis { get; set; }
        public int? BrojDanaKasnjenja { get; set; }

        public Posudba? Borrow { get; set; }
        public MarketUserEntity User { get; set; }
    }
}
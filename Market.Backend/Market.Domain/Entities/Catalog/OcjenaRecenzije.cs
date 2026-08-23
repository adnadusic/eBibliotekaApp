using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class OcjenaRecenzije : BaseEntity
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public ReviewRatingType TipOcjene { get; set; }
        public DateTime Datum { get; set; }

        public MarketUserEntity User { get; set; }
        public Recenzija Review { get; set; }
    }
}
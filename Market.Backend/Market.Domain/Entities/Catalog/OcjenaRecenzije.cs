using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public sealed class OcjenaRecenzije : BaseEntity
    {
        public int RecenzijaId { get; set; }
        public Recenzija Recenzija { get; set; }

        public int UserId { get; set; }
        public MarketUserEntity User { get; set; }

        // Preporuka: 1 = like, -1 = dislike
        public int Vrijednost { get; set; }
    }
}

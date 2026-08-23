using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public class ListaZelja : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int? Prioritet { get; set; }
        public string Napomena { get; set; }

        public MarketUserEntity User { get; set; }
        public Knjiga Book { get; set; }
    }
}
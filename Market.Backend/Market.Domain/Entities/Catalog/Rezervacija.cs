using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Rezervacija : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public DateTime DatumIsteka { get; set; }
        public ReservationStatus Status { get; set; }
        public int? Prioritet { get; set; }

        public MarketUserEntity User { get; set; }
        public Knjiga Book { get; set; }

        public ICollection<Posudba> Posudbe { get; set; }
    }
}
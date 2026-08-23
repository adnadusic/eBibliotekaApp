using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public class Recenzija : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Ocjena { get; set; }
        public string Naslov { get; set; }
        public string Komentar { get; set; }
        public DateTime Datum { get; set; }
        public int? BrojHelpful { get; set; }
        public int? BrojUnhelpful { get; set; }
        public bool? Izmijenjeno { get; set; }
        public DateTime? DatumIzmjene { get; set; }

        public MarketUserEntity User { get; set; }
        public Knjiga Book { get; set; }

        public ICollection<OcjenaRecenzije> Ocjene { get; set; }
    }
}
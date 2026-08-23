using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Obavijest : BaseEntity
    {
        public int UserId { get; set; }
        public NotificationType Tip { get; set; }
        public string Naslov { get; set; }
        public string Poruka { get; set; }
        public DateTime DatumSlanja { get; set; }
        public bool? Procitano { get; set; }
        public DateTime? DatumCitanja { get; set; }
        public int? VezanoZaId { get; set; }
        public string VezanoZaTip { get; set; }

        public MarketUserEntity User { get; set; }
    }
}
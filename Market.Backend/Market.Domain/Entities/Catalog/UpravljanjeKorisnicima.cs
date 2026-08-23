using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class UpravljanjeKorisnicima : BaseEntity
    {
        public int AdminId { get; set; }
        public int TargetUserId { get; set; }
        public UserManagementActionType TipAkcije { get; set; }
        public string Razlog { get; set; }
        public DateTime Datum { get; set; }
        public int? TrajanjeDana { get; set; }
        public string DodatneNapomene { get; set; }

        public MarketUserEntity Admin { get; set; }
        public MarketUserEntity TargetUser { get; set; }
    }
}
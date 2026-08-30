using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class UserManagementLog : BaseEntity
    {
        public int AdminId { get; set; }
        public int TargetUserId { get; set; }

        public UserManagementActionType ActionType { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DurationDays { get; set; }
        public string AdditionalNotes { get; set; }

        public MarketUserEntity Admin { get; set; }
        public MarketUserEntity TargetUser { get; set; }
    }
}
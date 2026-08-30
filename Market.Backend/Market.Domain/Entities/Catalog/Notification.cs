using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public NotificationType Type { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public MarketUserEntity User { get; set; }
    }
}
using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog;

public class NotificationSetting : BaseEntity
{
    public int UserId { get; set; }

    public NotificationType Type { get; set; }

    public bool IsPriority { get; set; }

    public MarketUserEntity User { get; set; } = null!;
}
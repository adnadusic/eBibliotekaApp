using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog;

public class PostavkaObavijesti : BaseEntity
{
    public int UserId { get; set; }

    public NotificationType Tip { get; set; }

    public bool Prioritetna { get; set; }

    public MarketUserEntity User { get; set; } = null!;
}
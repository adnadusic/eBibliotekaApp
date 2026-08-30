using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class ReviewReaction : BaseEntity
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public ReviewRatingType ReactionType { get; set; }
        public DateTime CreatedAt { get; set; }

        public MarketUserEntity User { get; set; }
        public Review Review { get; set; }
    }
}
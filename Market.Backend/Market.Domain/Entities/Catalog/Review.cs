using Market.Domain.Common;
using Market.Domain.Entities.Identity;

namespace Market.Domain.Entities.Catalog
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public int Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public int HelpfulCount { get; set; }
        public int UnhelpfulCount { get; set; }
        public bool IsEdited { get; set; }

        public DateTime? EditedAt { get; set; }

        public MarketUserEntity User { get; set; }
        public Book Book { get; set; }

        public ICollection<ReviewReaction> Reactions { get; set; }
    }
}
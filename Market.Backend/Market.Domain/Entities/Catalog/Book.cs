using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int? PublicationYear { get; set; }
        public int? PageCount { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string Description { get; set; }
        public string CoverImage { get; set; }

        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public int ViewCount { get; set; }

        public DateTime? AddedAt { get; set; }

        public ICollection<BookCopy> Copies { get; set; }
        public ICollection<BookAuthor> Authors { get; set; }
        public ICollection<BookGenre> Genres { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
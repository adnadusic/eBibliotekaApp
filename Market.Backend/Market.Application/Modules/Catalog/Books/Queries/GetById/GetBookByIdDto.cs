namespace Market.Application.Modules.Catalog.Books.Queries.GetById;

/// <summary>
/// Represents detailed information about a book.
/// </summary>
public sealed class GetBookByIdDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public int? PublicationYear { get; set; }
    public int? PageCount { get; set; }
    public int LanguageId { get; set; }
    public int? PublisherId { get; set; }
    public string? Description { get; set; }
    public string? CoverImage { get; set; }
    public int? TotalCopies { get; set; }
    public int? AvailableCopies { get; set; }
    public decimal? AverageRating { get; set; }
    public int? RatingCount { get; set; }
    public int? ViewCount { get; set; }
    public DateTime? AddedAt { get; set; }

    public List<int> AuthorIds { get; set; } = [];
    public List<int> GenreIds { get; set; } = [];
}
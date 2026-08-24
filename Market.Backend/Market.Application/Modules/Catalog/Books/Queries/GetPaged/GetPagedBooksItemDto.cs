namespace Market.Application.Modules.Catalog.Books.Queries.GetPaged;

/// <summary>
/// Represents a single book in the paged book list.
/// </summary>
public sealed class GetPagedBooksItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public int? PublicationYear { get; set; }
    public int? PageCount { get; set; }
    public int LanguageId { get; set; }
    public int? PublisherId { get; set; }
    public int? AvailableCopies { get; set; }
    public decimal? AverageRating { get; set; }
}
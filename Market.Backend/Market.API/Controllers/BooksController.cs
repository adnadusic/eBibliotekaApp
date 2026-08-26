using Market.Application.Common;
using Market.Application.Modules.Catalog.Books.Commands.Create;
using Market.Application.Modules.Catalog.Books.Commands.Delete;
using Market.Application.Modules.Catalog.Books.Commands.Update;
using Market.Application.Modules.Catalog.Books.Queries.GetById;
using Market.Application.Modules.Catalog.Books.Queries.GetPaged;

[ApiController]
[Route("api/books")]
public sealed class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PageResult<GetPagedBooksItemDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null,
        [FromQuery] string? isbn = null,
        [FromQuery] int? authorId = null,
        [FromQuery] int? genreId = null,
        [FromQuery] int? languageId = null,
        [FromQuery] string sortBy = "title",
        [FromQuery] string sortDirection = "asc",
        CancellationToken ct = default)
    {
        var query = new GetPagedBooksQuery
        {
            Paging = new PageRequest
            {
                Page = page,
                PageSize = pageSize
            },
            Title = title,
            Isbn = isbn,
            AuthorId = authorId,
            GenreId = genreId,
            LanguageId = languageId,
            SortBy = sortBy,
            SortDirection = sortDirection
        };

        return Ok(await mediator.Send(query, ct));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<GetBookByIdDto>> GetById(
        int id,
        CancellationToken ct)
    {
        return Ok(await mediator.Send(
            new GetBookByIdQuery { Id = id },
            ct));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateBookCommandDto>> Create(
        [FromBody] CreateBookCommand command,
        CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UpdateBookCommandDto>> Update(
        [FromBody] UpdateBookCommand command,
        CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct)
    {
        await mediator.Send(
            new DeleteBookCommand { Id = id },
            ct);

        return NoContent();
    }
}
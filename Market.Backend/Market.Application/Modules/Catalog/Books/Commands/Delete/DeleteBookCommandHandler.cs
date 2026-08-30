using Market.Application.Modules.Catalog.Books.Commands.Delete;

public sealed class DeleteBookCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser)
    : IRequestHandler<DeleteBookCommand>
{
    public async Task Handle(
        DeleteBookCommand request,
        CancellationToken ct)
    {
        if (!currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only administrators can delete books.");
        }

        var book = await ctx.Books
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                ct)
            ?? throw new MarketNotFoundException(
                "Book was not found.");

        book.IsDeleted = true;
        book.ModifiedAtUtc = DateTime.UtcNow;

        await ctx.SaveChangesAsync(ct);
    }
}
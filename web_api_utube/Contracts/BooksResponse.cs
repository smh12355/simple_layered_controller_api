namespace web_api_utube.Contracts
{
    // какие мы поля их всех будем возвращать
    public record BooksResponse(Guid id, string Title, string Description, decimal Price);
    public record BooksRequest(string Title, string Description, decimal Price);
}

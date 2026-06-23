namespace MinimalAPIRealProject.Models.Entity
{
    public record BookRequest(string Isbn, string Title, string ShortDescription, int PageCount, string PublishDate);
}

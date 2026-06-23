namespace MinimalAPIRealProject.Models.Entity
{
    public record BookResponse(int Id, string Isbn, string Title, string ShortDescription, int PageCount, DateTime PublishDate);
}

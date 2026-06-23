namespace MinimalAPIRealProject.Models.DTO
{
    public record BookDto(string Isbn, string Title, string ShortDescription, int PageCount, DateTime PublishDate);
}

using MinimalAPIRealProject.Models.Entity;

namespace MinimalAPIRealProject.Repository
{
    public interface IOperationRepository
    {
        Task<(Dictionary<int, BookResponse> lst, string err)> GetBooksListRepo(CancellationToken cancellationToken);
        Task<(BookResponse lst, string err)> GetBookRepo(string isbn, CancellationToken cancellationToken);
    }
}

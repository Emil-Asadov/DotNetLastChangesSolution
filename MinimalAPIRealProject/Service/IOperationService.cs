using MinimalAPIRealProject.Models.DTO;
using MinimalAPIRealProject.Models.Entity;

namespace MinimalAPIRealProject.Service
{
    public interface IOperationService
    {
        Task<(Dictionary<int, BookDto> lst, string err)> GetBooksListSrv(CancellationToken cancellationToken);
        Task<(BookDto lst, string err)> GetBookSrv(string isbn, CancellationToken cancellationToken);
        Task<(string res, string err)> PostBookSrv(BookRequest bookRequest, CancellationToken cancellationToken);
        Task<(string res, string err)> UpdateBookSrv(int id, BookRequest bookRequest, CancellationToken cancellationToken);
        Task<(string res, string err)> DeleteBookSrv(int id, CancellationToken cancellationToken);
    }
}

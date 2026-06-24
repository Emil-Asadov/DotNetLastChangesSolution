using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.Models.DTO;
using MinimalAPIRealProject.Models.Entity;
using MinimalAPIRealProject.Repository;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;

namespace MinimalAPIRealProject.Service
{
    public sealed class OperationService(IOperationRepository operationRepository, DbOperation dbOperation) : IOperationService
    {
        //private readonly OperationRepository _operationRepository;
        //private readonly DbOperation _dbOperation;
        //public OperationService(OperationRepository operationRepository, DbOperation dbOperation)
        //{
        //    _operationRepository = operationRepository;
        //    _dbOperation = dbOperation;
        //}
        public async Task<(Dictionary<int, BookDto> lst, string err)> GetBooksListSrv(CancellationToken cancellationToken)
        {
            Dictionary<int, BookDto> lstOperationId = new();
            var errOperation = string.Empty;
            var operationDtoCollection = await operationRepository.GetBooksListRepo(cancellationToken);
            errOperation = operationDtoCollection.err;
            if (!string.IsNullOrEmpty(errOperation))
                return (lstOperationId, errOperation);

            #region LINQ version
            lstOperationId = operationDtoCollection.lst.ToDictionary(x => x.Key, x => new BookDto
            (
                Isbn: x.Value.Isbn,
                Title: x.Value.Title,
                ShortDescription: x.Value.ShortDescription,
                PageCount: x.Value.PageCount,
                PublishDate: x.Value.PublishDate
            ));
            #endregion

            return (lstOperationId, errOperation);
        }

        public async Task<(BookDto lst, string err)> GetBookSrv(string isbn, CancellationToken cancellationToken)
        {
            BookDto cls = null!;
            var errOperation = string.Empty;
            var operationDtoCollection = await operationRepository.GetBookRepo(isbn, cancellationToken);
            errOperation = operationDtoCollection.err;
            if (!string.IsNullOrEmpty(errOperation))
                return (cls, errOperation);

            #region LINQ version
            cls = new BookDto
            (
                Isbn: operationDtoCollection.lst.Isbn,
                Title: operationDtoCollection.lst.Title,
                ShortDescription: operationDtoCollection.lst.ShortDescription,
                PageCount: operationDtoCollection.lst.PageCount,
                PublishDate: operationDtoCollection.lst.PublishDate
            );
            #endregion

            return (cls, errOperation);
        }

        public async Task<(string res, string err)> PostBookSrv(BookRequest bookRequest, CancellationToken cancellationToken)
        {
            var resOperation = string.Empty;
            var errOperation = string.Empty;
            try
            {
                var com = new OracleCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BNK_RBEEMIL.PKG_BOOKS.CREATE_BOOK"
                };
                com.Parameters.Add("P_ISBN", OracleDbType.Varchar2).Value = bookRequest.Isbn;
                com.Parameters.Add("P_TITLE", OracleDbType.Varchar2).Value = bookRequest.Title;
                com.Parameters.Add("P_SHORT_DESCRIPTION", OracleDbType.Varchar2).Value = bookRequest.ShortDescription;
                com.Parameters.Add("P_PAGE_COUNT", OracleDbType.Int32).Value = bookRequest.PageCount;
                com.Parameters.Add("P_PUBLISH_DATE", OracleDbType.Date).Value = DateTime.ParseExact(bookRequest.PublishDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                var pRes = new OracleParameter
                {
                    ParameterName = "RES",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.Varchar2,
                    Size = 100
                };
                com.Parameters.Add(pRes);

                errOperation = await dbOperation.PostData(com, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOperation))
                    resOperation = pRes.Value.ToString();
            }
            catch (Exception ex)
            {
                errOperation = ex.Message;
            }

            return (resOperation!, errOperation);
        }

        public async Task<(string res, string err)> UpdateBookSrv(int id, BookRequest bookRequest, CancellationToken cancellationToken)
        {
            var resOperation = string.Empty;
            var errOperation = string.Empty;
            try
            {
                var com = new OracleCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BNK_RBEEMIL.PKG_BOOKS.UPDATE_BOOK"
                };
                com.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                com.Parameters.Add("P_ISBN", OracleDbType.Varchar2).Value = bookRequest.Isbn;
                com.Parameters.Add("P_TITLE", OracleDbType.Varchar2).Value = bookRequest.Title;
                com.Parameters.Add("P_SHORT_DESCRIPTION", OracleDbType.Varchar2).Value = bookRequest.ShortDescription;
                com.Parameters.Add("P_PAGE_COUNT", OracleDbType.Int32).Value = bookRequest.PageCount;
                com.Parameters.Add("P_PUBLISH_DATE", OracleDbType.Date).Value = DateTime.ParseExact(bookRequest.PublishDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                var pRes = new OracleParameter
                {
                    ParameterName = "RES",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.Varchar2,
                    Size = 100
                };
                com.Parameters.Add(pRes);

                errOperation = await dbOperation.PostData(com, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOperation))
                    resOperation = pRes.Value.ToString();
            }
            catch (Exception ex)
            {
                errOperation = ex.Message;
            }

            return (resOperation!, errOperation);
        }

        public async Task<(string res, string err)> DeleteBookSrv(int id, CancellationToken cancellationToken)
        {
            var resOperation = string.Empty;
            var errOperation = string.Empty;
            try
            {
                var com = new OracleCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BNK_RBEEMIL.PKG_BOOKS.DELETE_BOOK"
                };
                com.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;

                var pRes = new OracleParameter
                {
                    ParameterName = "RES",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.Varchar2,
                    Size = 100
                };
                com.Parameters.Add(pRes);

                errOperation = await dbOperation.PostData(com, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOperation))
                    resOperation = pRes.Value.ToString();
            }
            catch (Exception ex)
            {
                errOperation = ex.Message;
            }

            return (resOperation!, errOperation);
        }
    }
}

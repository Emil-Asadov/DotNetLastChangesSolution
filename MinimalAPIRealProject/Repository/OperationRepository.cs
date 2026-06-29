using MinimalAPIRealProject.DB;
using MinimalAPIRealProject.Models.Entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MinimalAPIRealProject.Repository
{
    public sealed class OperationRepository(DbOperation dbOperation) : IOperationRepository
    {
        public async Task<(Dictionary<int, BookResponse> lst, string err)> GetBooksListRepo(CancellationToken cancellationToken)
        {
            var lst = new Dictionary<int, BookResponse>();
            var dtOut = new DataTable();
            var errOut = string.Empty;
            var query = "BNK_RBEEMIL.PKG_BOOKS.GET_BOOKS";
            try
            {
                var vRes = new OracleParameter
                {
                    ParameterName = "V_RET",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };

                (dtOut, errOut) = await dbOperation.GetData(query, new OracleParameter[] { vRes }, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOut))
                {
                    for (int i = 0; i < dtOut.Rows.Count; i++)
                    {
                        lst.Add(i, new BookResponse
                        (
                            Id: Convert.ToInt32(dtOut.Rows[i].Field<decimal>("ID")),
                            Isbn: dtOut.Rows[i].Field<string>("ISBN") ?? string.Empty,
                            Title: dtOut.Rows[i].Field<string>("TITLE") ?? string.Empty,
                            ShortDescription: dtOut.Rows[i].Field<string>("SHORT_DESCRIPTION") ?? string.Empty,
                            PageCount: Convert.ToInt32(dtOut.Rows[i].Field<decimal>("PAGE_COUNT")),
                            PublishDate: Convert.ToDateTime(dtOut.Rows[i].Field<DateTime>("PUBLISH_DATE"))
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }

            return (lst, errOut);
        }

        public async Task<(BookResponse lst, string err)> GetBookRepo(string isbn, CancellationToken cancellationToken)
        {
            BookResponse cls = null!;
            var dtOut = new DataTable();
            var errOut = string.Empty;
            var query = "BNK_RBEEMIL.PKG_BOOKS.GET_BOOK";
            try
            {
                var vRes = new OracleParameter
                {
                    ParameterName = "V_RET",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };

                var pOperationID = new OracleParameter
                {
                    ParameterName = "P_ISBN",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = isbn
                };

                (dtOut, errOut) = await dbOperation.GetData(query, new OracleParameter[] { vRes, pOperationID }, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOut))
                {
                    cls = new BookResponse
                          (
                              Id: Convert.ToInt32(dtOut.Rows[0].Field<decimal>("ID")),
                              Isbn: dtOut.Rows[0].Field<string>("ISBN") ?? string.Empty,
                              Title: dtOut.Rows[0].Field<string>("TITLE") ?? string.Empty,
                              ShortDescription: dtOut.Rows[0].Field<string>("SHORT_DESCRIPTION") ?? string.Empty,
                              PageCount: Convert.ToInt32(dtOut.Rows[0].Field<decimal>("PAGE_COUNT")),
                              PublishDate: Convert.ToDateTime(dtOut.Rows[0].Field<DateTime>("PUBLISH_DATE"))
                          );
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }

            return (cls, errOut);
        }

        public async Task<(UserResponse userResponse, string err)> GetUserRepo(UserRequest userRequest, CancellationToken cancellationToken)
        {
            UserResponse cls = null!;
            var dtOut = new DataTable();
            var errOut = string.Empty;
            var query = "BNK_RBEEMIL.PKG_BOOKS.GET_USER";
            try
            {
                var vRes = new OracleParameter
                {
                    ParameterName = "V_RET",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };

                var pName = new OracleParameter
                {
                    ParameterName = "P_NAME",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = userRequest.Name
                };

                var pSurname = new OracleParameter
                {
                    ParameterName = "P_SURNAME",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = userRequest.Surname
                };

                var pPassword = new OracleParameter
                {
                    ParameterName = "P_PASSWORD",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = userRequest.Password
                };

                (dtOut, errOut) = await dbOperation.GetData(query, new OracleParameter[] { vRes, pName, pSurname, pPassword }, cancellationToken);
                if (string.IsNullOrWhiteSpace(errOut))
                {
                    if (!dtOut.Rows.Count.Equals(0))
                    {
                        cls = new UserResponse
                           (
                                  Id: Convert.ToInt32(dtOut.Rows[0].Field<decimal>("ID")),
                                  Name: dtOut.Rows[0].Field<string>("NAME")!,
                                  Surname: dtOut.Rows[0].Field<string>("SURNAME")!,
                                  Role: dtOut.Rows[0].Field<string>("ROLE")!,
                                  Password: dtOut.Rows[0].Field<string>("PASSWORD")!
                           );
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }

            return (cls, errOut);
        }
    }
}

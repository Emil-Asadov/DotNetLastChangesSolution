using Microsoft.Extensions.Options;
using MinimalAPIRealProject.Models.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace MinimalAPIRealProject.DB
{
    public class DbOperation
    {
        private readonly DbConnect _dbConnect;
        private readonly DbConfigRecord _dbConfigRecord;
        public DbOperation(DbConnect dbConnect, IOptions<DbConfigRecord> dbConfigRecord)
        {
            _dbConnect = dbConnect;
            _dbConfigRecord = dbConfigRecord.Value;
        }

        public async Task<(DataTable dt, string err)> GetData(string query, OracleParameter[]? parameterCollection,CancellationToken cancellationToken)
        {
            var err = string.Empty;
            var dt = new DataTable();
            OracleConnection? con = null;
            try
            {
                (con, err) = await _dbConnect.GetConnection(_dbConnect.GetConnectionString(_dbConfigRecord),cancellationToken);
                if (!string.IsNullOrWhiteSpace(err))
                    return (dt, err);
                using (con)
                {
                    using (OracleCommand com = con.CreateCommand())
                    {
                        com.CommandText = query;
                        com.CommandType = CommandType.StoredProcedure;
                        if (parameterCollection != null)
                            com.Parameters.AddRange(parameterCollection);
                        using (OracleDataReader read = await com.ExecuteReaderAsync())
                        {
                            dt.Load(read);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return (dt, err);
        }

        public async Task<string> PostData(OracleCommand ocm,CancellationToken cancellationToken)
        {
            var err = string.Empty;
            try
            {
                var (conn, connErr) = await _dbConnect.GetConnection(_dbConnect.GetConnectionString(_dbConfigRecord),cancellationToken);
                if (!string.IsNullOrWhiteSpace(connErr) || conn is null)
                    return connErr;

                await using (conn)
                {
                    using var myTrans = conn.BeginTransaction();
                    ocm.Connection = conn;
                    ocm.Transaction = myTrans;
                    try
                    {
                        await ocm.ExecuteNonQueryAsync();
                        myTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        err = ex.Message;
                        myTrans.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return err;
        }
    }
}

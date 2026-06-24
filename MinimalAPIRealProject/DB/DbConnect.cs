using MinimalAPIRealProject.Models.Config;
using Oracle.ManagedDataAccess.Client;

namespace MinimalAPIRealProject.DB
{
    public sealed class DbConnect
    {
        public async Task<(OracleConnection? con, string err)> GetConnection(string connectionString, CancellationToken cancellationToken)
        {
            OracleConnection? con = null;
            try
            {
                con = new OracleConnection(connectionString);
                await con.OpenAsync();

                return (con, string.Empty);
            }
            catch (Exception ex)
            {
                con?.Dispose();
                return (null, ex.Message);
            }
        }

        public string GetConnectionString(DbConfigRecord cls)
        {
            var connectionString = $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {cls.Hostname})(PORT = {cls.Port}))(CONNECT_DATA =(SERVER = dedicated)(SERVICE_NAME = {cls.ServiceName})));Password={cls.Password};User ID={cls.Username}";

            return connectionString;
        }
    }
}

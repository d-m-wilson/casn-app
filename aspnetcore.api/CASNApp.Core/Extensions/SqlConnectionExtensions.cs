using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CASNApp.Core.Extensions
{
    public static class SqlConnectionExtensions
    {
        public static async Task<int> ExecuteStoredProcedureAsync(this SqlConnection sqlConnection, string procedureName, SqlParameter[] parameters)
        {
            using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(parameters);
                var returnValue = await sqlCommand.ExecuteScalarAsync();
                return (int)returnValue;
            }
        }

    }
}

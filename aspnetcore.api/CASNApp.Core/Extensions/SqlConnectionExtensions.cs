using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CASNApp.Core.Extensions
{
    public static class SqlConnectionExtensions
    {
        public static async Task<int> ExecuteStoredProcedureAsync(this SqlConnection sqlConnection, string procedureName, SqlParameter[] parameters)
        {
            SqlParameter returnValue;

            using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(parameters);

                returnValue = new SqlParameter
                {
                    ParameterName = "ReturnValue",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue,
                };

                sqlCommand.Parameters.Add(returnValue);

                var openedConnection = false;

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    await sqlConnection.OpenAsync();
                    openedConnection = true;
                }

                try
                {
                    await sqlCommand.ExecuteNonQueryAsync();

                    return (int)returnValue.Value;
                }
                finally
                {
                    if (openedConnection)
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

    }
}

using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CASNApp.DriveExpirationManager
{
    class Program
    {
        private static readonly IConfiguration configuration;
        private static readonly string driveExpirationProcedureName;

        static Program()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<Program>(true)
                .AddEnvironmentVariables()
                .Build();

            driveExpirationProcedureName = configuration["DriveExpirationProcedureName"];
        }

        static void Main(string[] args)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("DbConnectionString")))
                {
                    sqlConnection.Open();

                    using (var sqlCommand = new SqlCommand(driveExpirationProcedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Fatal Exception: {ex}");
                throw new Exception("Re-throwing exception. ", ex);
            }
        }
    }
}
